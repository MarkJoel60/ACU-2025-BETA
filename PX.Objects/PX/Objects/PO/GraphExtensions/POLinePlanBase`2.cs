// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POLinePlanBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions;

public abstract class POLinePlanBase<TGraph, TItemPlanSource> : 
  ItemPlan<TGraph, PX.Objects.PO.POOrder, TItemPlanSource>
  where TGraph : PXGraph
  where TItemPlanSource : class, IItemPlanPOSource, IBqlTable, new()
{
  public override INItemPlan DefaultValues(INItemPlan planRow, TItemPlanSource origRow)
  {
    if (EnumerableExtensions.IsNotIn<string>(origRow.OrderType, "RO", "DP", "BL") || !origRow.InventoryID.HasValue || !origRow.SiteID.HasValue || origRow.Cancelled.GetValueOrDefault() || origRow.Completed.GetValueOrDefault() && !this.CreatePlanForCompletedLine(origRow))
      return (INItemPlan) null;
    PX.Objects.PO.POOrder current = (PX.Objects.PO.POOrder) ((PXCache) GraphHelper.Caches<PX.Objects.PO.POOrder>((PXGraph) this.Base)).Current;
    PX.Objects.PO.POOrder order = !(current?.OrderType == origRow.OrderType) || !(current.OrderNbr == origRow.OrderNbr) ? PXParentAttribute.SelectParent<PX.Objects.PO.POOrder>((PXCache) this.ItemPlanSourceCache, (object) origRow) : current;
    bool isOnHold = this.IsOrderOnHold(order);
    string newPlanType;
    if (!this.TryCalcPlanType((PXCache) this.ItemPlanSourceCache, origRow, isOnHold, out newPlanType))
      return (INItemPlan) null;
    planRow.PlanType = newPlanType;
    planRow.BAccountID = origRow.VendorID;
    planRow.InventoryID = origRow.InventoryID;
    planRow.SubItemID = origRow.SubItemID;
    planRow.SiteID = origRow.SiteID;
    planRow.ProjectID = origRow.ProjectID;
    planRow.TaskID = origRow.TaskID;
    planRow.CostCenterID = origRow.CostCenterID;
    planRow.PlanDate = origRow.PromisedDate;
    planRow.UOM = origRow.UOM;
    planRow.PlanQty = origRow.BaseOpenQty;
    planRow.RefNoteID = order.NoteID;
    planRow.Hold = new bool?(isOnHold);
    return string.IsNullOrEmpty(planRow.PlanType) ? (INItemPlan) null : planRow;
  }

  protected virtual bool IsOrderOnHold(PX.Objects.PO.POOrder order)
  {
    return order != null && EnumerableExtensions.IsNotIn<string>(order.Status, "A", "N", "M", "C");
  }

  protected virtual bool TryCalcPlanType(
    PXCache sender,
    TItemPlanSource line,
    bool isOnHold,
    out string newPlanType)
  {
    newPlanType = (string) null;
    if (line.OrderType == "BL")
    {
      newPlanType = "7B";
    }
    else
    {
      string lineType = line.LineType;
      if (lineType != null && lineType.Length == 2)
      {
        switch (lineType[1])
        {
          case 'F':
            if (lineType == "GF" || lineType == "NF")
            {
              newPlanType = isOnHold ? "F8" : "F7";
              goto label_16;
            }
            goto label_16;
          case 'I':
            if (lineType == "GI")
              goto label_14;
            goto label_16;
          case 'M':
            if (lineType == "GM" || lineType == "NM")
            {
              newPlanType = isOnHold ? "M3" : "M4";
              goto label_16;
            }
            goto label_16;
          case 'O':
            if (lineType == "NO")
              break;
            goto label_16;
          case 'P':
            if (lineType == "GP" || lineType == "NP")
            {
              newPlanType = isOnHold ? "79" : "74";
              goto label_16;
            }
            goto label_16;
          case 'R':
            if (lineType == "GR")
              goto label_14;
            goto label_16;
          case 'S':
            switch (lineType)
            {
              case "GS":
                break;
              case "NS":
                goto label_14;
              default:
                goto label_16;
            }
            break;
          default:
            goto label_16;
        }
        newPlanType = isOnHold ? "78" : "76";
        goto label_16;
label_14:
        newPlanType = isOnHold ? "73" : "70";
      }
    }
label_16:
    return newPlanType != null;
  }

  protected override void SetPlanID(TItemPlanSource row, long? planID)
  {
    base.SetPlanID(row, planID);
    row.ClearPlanID = new bool?(false);
  }

  protected override void ClearPlanID(TItemPlanSource row)
  {
    if (row.PlanID.HasValue)
    {
      long? planId = row.PlanID;
      long num = 0;
      if (!(planId.GetValueOrDefault() < num & planId.HasValue))
      {
        row.ClearPlanID = new bool?(true);
        return;
      }
    }
    base.ClearPlanID(row);
  }

  public override void _(PX.Data.Events.RowPersisting<TItemPlanSource> e)
  {
    if (e.Row.ClearPlanID.GetValueOrDefault())
    {
      base.ClearPlanID(e.Row);
      e.Row.ClearPlanID = new bool?();
    }
    base._(e);
  }

  protected virtual bool CreatePlanForCompletedLine(TItemPlanSource line)
  {
    if (!EnumerableExtensions.IsNotIn<string>(line.LineType, "GS", "NO"))
    {
      bool? completed = line.Completed;
      bool flag = false;
      if (!(completed.GetValueOrDefault() == flag & completed.HasValue))
        return PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, Equal<False>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.AsOptional>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.isCorrection, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.isAdjustedIN, IBqlBool>.IsEqual<True>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[3]
        {
          (object) line.OrderType,
          (object) line.OrderNbr,
          (object) line.LineNbr
        })) != null;
    }
    return false;
  }
}
