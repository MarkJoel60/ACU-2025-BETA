// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.Special
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class Special : PXGraphExtension<POOrderEntry.MultiCurrency, POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXAction) ((PXGraphExtension<POOrderEntry>) this).Base.Delete).SetDynamicText(true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    if (e.Row == null)
      return;
    int? specialLineCntr = e.Row.SpecialLineCntr;
    int num = 0;
    if (specialLineCntr.GetValueOrDefault() > num & specialLineCntr.HasValue)
      ((PXAction) ((PXGraphExtension<POOrderEntry>) this).Base.Delete).SetConfirmationMessage("At least one line with a special-order item in the purchase order is linked to a sales order. Do you want to delete the purchase order?");
    else
      ((PXAction) ((PXGraphExtension<POOrderEntry>) this).Base.Delete).SetConfirmationMessage("The current {0} record will be deleted.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.cancelled> e)
  {
    PX.Objects.PO.POOrder row = e.Row;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      int? specialLineCntr = row.SpecialLineCntr;
      int num2 = 0;
      num1 = specialLineCntr.GetValueOrDefault() > num2 & specialLineCntr.HasValue ? 1 : 0;
    }
    if (num1 == 0)
      return;
    bool? nullable = e.Row.Cancelled;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.cancelled>, PX.Objects.PO.POOrder, object>) e).NewValue;
    if (!nullable.GetValueOrDefault() || ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Ask("Warning", "At least one purchase order line with a special-order item is linked to a line of a sales order. Do you want to cancel the purchase order?", (MessageButtons) 4) == 6)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.cancelled>, PX.Objects.PO.POOrder, object>) e).NewValue = (object) false;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.cancelled>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POLine> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.IsSpecialOrder;
    if (nullable.GetValueOrDefault())
    {
      string costChangedMessage = this.GetUnitCostChangedMessage(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, e.Row);
      if (PXUIFieldAttribute.GetErrorOnly<PX.Objects.PO.POLine.curyUnitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row) == null)
        PXUIFieldAttribute.SetWarning<PX.Objects.PO.POLine.curyUnitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, costChangedMessage);
    }
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    nullable = e.Row.IsSpecialOrder;
    if (nullable.GetValueOrDefault())
    {
      nullable = e.Row.Completed;
      if (!nullable.GetValueOrDefault())
      {
        POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
        if (soLine != null)
        {
          nullable = soLine.Completed;
          if (nullable.GetValueOrDefault())
          {
            propertyException = new PXSetPropertyException("The corresponding line in the linked {0} {1} sales order has been completed.", (PXErrorLevel) 3, new object[2]
            {
              (object) soLine.OrderType,
              (object) soLine.OrderNbr
            });
            goto label_12;
          }
          goto label_12;
        }
        goto label_12;
      }
    }
    nullable = e.Row.SODeleted;
    if (nullable.GetValueOrDefault())
    {
      nullable = e.Row.Completed;
      if (!nullable.GetValueOrDefault())
        propertyException = new PXSetPropertyException("The linked sales order line has been deleted.", (PXErrorLevel) 3);
    }
label_12:
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.isSpecialOrder>((object) e.Row, (object) true, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost> e)
  {
    if (!e.Row.IsSpecialOrder.GetValueOrDefault())
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>, PX.Objects.PO.POLine, object>) e).NewValue;
    Decimal? curyUnitCost = e.Row.CuryUnitCost;
    if (newValue.GetValueOrDefault() == curyUnitCost.GetValueOrDefault() & newValue.HasValue == curyUnitCost.HasValue)
      return;
    POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
    if (soLine != null && soLine.Completed.GetValueOrDefault())
      throw new PXSetPropertyException("The unit cost cannot be changed because the corresponding line in the {0} {1} sales order has been completed.", new object[2]
      {
        (object) soLine.OrderType,
        (object) soLine.OrderNbr
      });
    if (!this.IsSingleSpecialPOLine(e.Row))
      throw new PXSetPropertyException("The unit cost cannot be changed because the line is linked to a special-order line of a sales order, and this SO line is linked to multiple PO lines.", new object[2]
      {
        (object) soLine.OrderType,
        (object) soLine.OrderNbr
      });
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.uOM> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.uOM>, PX.Objects.PO.POLine, object>) e).NewValue))
    {
      POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
      throw new PXSetPropertyException("The line has been linked to the line of the {0} {1} sales order. To make changes in this column, unlink the purchase order line on the Sales Orders (SO301000) form.", new object[2]
      {
        (object) soLine?.OrderType,
        (object) soLine?.OrderNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>, PX.Objects.PO.POLine, object>) e).NewValue))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, e.Row.ProjectID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) pmProject?.ContractCD;
      POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
      throw new PXSetPropertyException("The line has been linked to the line of the {0} {1} sales order. To make changes in this column, unlink the purchase order line on the Sales Orders (SO301000) form.", new object[2]
      {
        (object) soLine?.OrderType,
        (object) soLine?.OrderNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.taskID> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.taskID>, PX.Objects.PO.POLine, object>) e).NewValue))
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, e.Row.ProjectID, e.Row.TaskID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.taskID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) pmTask?.TaskCD;
      POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
      throw new PXSetPropertyException("The line has been linked to the line of the {0} {1} sales order. To make changes in this column, unlink the purchase order line on the Sales Orders (SO301000) form.", new object[2]
      {
        (object) soLine?.OrderType,
        (object) soLine?.OrderNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.costCodeID> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.costCodeID>, PX.Objects.PO.POLine, object>) e).NewValue))
    {
      PMCostCode pmCostCode = PMCostCode.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, e.Row.CostCodeID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.costCodeID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) pmCostCode?.CostCodeCD;
      POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
      throw new PXSetPropertyException("The line has been linked to the line of the {0} {1} sales order. To make changes in this column, unlink the purchase order line on the Sales Orders (SO301000) form.", new object[2]
      {
        (object) soLine?.OrderType,
        (object) soLine?.OrderNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    bool? nullable = e.Row.Cancelled;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled>, PX.Objects.PO.POLine, object>) e).NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current;
    int num;
    if (current == null)
    {
      num = 1;
    }
    else
    {
      nullable = current.Cancelled;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0 || ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Ask("Warning", "The line with a special-order item is linked to a sales order line. Do you want to cancel this line?", (MessageButtons) 4) == 6)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled>, PX.Objects.PO.POLine, object>) e).NewValue = (object) false;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    bool? nullable = e.Row.Cancelled;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled>, PX.Objects.PO.POLine, object>) e).OldValue;
    if (nullable.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POLine, PX.Objects.PO.POLine.cancelled>>) e).Cache.SetValueExt<PX.Objects.PO.POLine.isSpecialOrder>((object) e.Row, (object) false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POLine> e)
  {
    if (!e.Row.IsSpecialOrder.GetValueOrDefault())
      return;
    Decimal? curyUnitCost1 = e.Row.CuryUnitCost;
    Decimal? curyUnitCost2 = e.OldRow.CuryUnitCost;
    if (curyUnitCost1.GetValueOrDefault() == curyUnitCost2.GetValueOrDefault() & curyUnitCost1.HasValue == curyUnitCost2.HasValue)
      return;
    POOrderEntry.SOLine5 soLine = this.GetSOLine(e.Row);
    POOrderEntry.SOLine5 soLine5_1 = soLine != null ? soLine : throw new RowNotFoundException((PXCache) GraphHelper.Caches<SOLineSplit>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base), new object[3]
    {
      (object) e.Row.OrderType,
      (object) e.Row.OrderNbr,
      (object) e.Row.LineNbr
    });
    curyUnitCost2 = soLine.CuryUnitCost;
    Decimal? nullable1 = e.Row.CuryUnitCost;
    bool? nullable2 = new bool?(!(curyUnitCost2.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyUnitCost2.HasValue == nullable1.HasValue));
    soLine5_1.IsCostUpdatedOnPO = nullable2;
    POOrderEntry.SOLine5 soLine5_2 = soLine;
    Decimal? nullable3;
    if (!soLine.IsCostUpdatedOnPO.GetValueOrDefault())
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = e.Row.CuryUnitCost;
    soLine5_2.CuryUnitCostUpdated = nullable3;
    ((PXSelectBase<POOrderEntry.SOLine5>) ((PXGraphExtension<POOrderEntry>) this).Base.FixedDemandOrigSOLine).Update(soLine);
    ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.PO.POLine> e)
  {
    if (!e.Row.IsSpecialOrder.GetValueOrDefault() || ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current) == 3 || ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Ask("Warning", "The line with a special-order item is linked to a sales order line. Do you want to delete this line?", (MessageButtons) 4) == 6)
      return;
    e.Cancel = true;
  }

  protected virtual string GetUnitCostChangedMessage(PXCache cache, PX.Objects.PO.POLine row)
  {
    PXDecimalState valueExt = (PXDecimalState) cache.GetValueExt<PX.Objects.PO.POLine.curyUnitCost>((object) row);
    int num1 = valueExt != null ? ((PXFieldState) valueExt).Precision : 2;
    Decimal? nullable = (Decimal?) cache.GetValueOriginal<PX.Objects.PO.POLine.curyUnitCost>((object) row);
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    Decimal num2 = valueOrDefault1;
    nullable = row.CuryUnitCost;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    if (num2 == valueOrDefault2 & nullable.HasValue)
      return (string) null;
    object[] objArray = new object[2]
    {
      (object) valueOrDefault1.ToString($"N{num1}"),
      null
    };
    nullable = row.CuryUnitCost;
    objArray[1] = (object) nullable.GetValueOrDefault().ToString($"N{num1}");
    return PXMessages.LocalizeFormatNoPrefix("The unit cost in the linked line of the sales order will be changed from {0} to {1} when you save the purchase order. To view the affected sales orders, click View SO Demand on the table toolbar.", objArray);
  }

  protected virtual POOrderEntry.SOLine5 GetSOLine(PX.Objects.PO.POLine poline)
  {
    return PXResultset<POOrderEntry.SOLine5>.op_Implicit(PXSelectBase<POOrderEntry.SOLine5, PXViewOf<POOrderEntry.SOLine5>.BasedOn<SelectFromBase<POOrderEntry.SOLine5, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POOrderEntry.SOLineSplit3>.On<POOrderEntry.SOLineSplit3.FK.OrderLine>>>.Where<KeysRelation<CompositeKey<Field<POOrderEntry.SOLineSplit3.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<POOrderEntry.SOLineSplit3.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<POOrderEntry.SOLineSplit3.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, POOrderEntry.SOLineSplit3>, PX.Objects.PO.POLine, POOrderEntry.SOLineSplit3>.SameAsCurrent>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
    {
      (object) poline
    }, Array.Empty<object>()));
  }

  protected virtual bool IsSingleSpecialPOLine(PX.Objects.PO.POLine poline)
  {
    return PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<SelectFromBase<SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POOrderEntry.SOLineSplit3>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrderEntry.SOLineSplit3.orderType, Equal<SOLineSplit.orderType>>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.orderNbr, IBqlString>.IsEqual<SOLineSplit.orderNbr>>>>.And<BqlOperand<POOrderEntry.SOLineSplit3.lineNbr, IBqlInt>.IsEqual<SOLineSplit.lineNbr>>>>>.Where<KeysRelation<CompositeKey<Field<SOLineSplit.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<SOLineSplit.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<SOLineSplit.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, SOLineSplit>, PX.Objects.PO.POLine, SOLineSplit>.And<KeysRelation<CompositeKey<Field<POOrderEntry.SOLineSplit3.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<POOrderEntry.SOLineSplit3.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<POOrderEntry.SOLineSplit3.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, POOrderEntry.SOLineSplit3>, PX.Objects.PO.POLine, POOrderEntry.SOLineSplit3>.SameAsCurrent>>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
    {
      (object) poline
    }, Array.Empty<object>()).Count == 1;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.AllowOverrideCury" />
  /// </summary>
  [PXOverride]
  public virtual bool AllowOverrideCury(Func<bool> baseMethod)
  {
    return baseMethod() && ((int?) ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current?.SpecialLineCntr).GetValueOrDefault() == 0;
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost> e,
    PXFieldDefaulting baseEventHandler)
  {
    PX.Objects.PO.POLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>, PX.Objects.PO.POLine, object>) e).NewValue = (object) e.Row.CuryUnitCost.GetValueOrDefault();
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>>) e).Cancel = true;
    }
    else
      baseEventHandler?.Invoke(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>>) e).Args);
  }

  [PXLocalizable]
  public static class ExtensionMessages
  {
    public const string SpecialLineExistsDeleteOrder = "At least one line with a special-order item in the purchase order is linked to a sales order. Do you want to delete the purchase order?";
  }
}
