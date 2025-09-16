// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CheckWorkCodeCostCodeRangeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.PM;

public class CheckWorkCodeCostCodeRangeAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber
{
  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is PMWorkCodeCostCodeRange row))
      return;
    this.Verify(sender, row);
  }

  public void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is PMWorkCodeCostCodeRange newRow))
      return;
    this.Verify(sender, newRow);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PMWorkCodeCostCodeRange row) || (e.Operation & 3) == 3)
      return;
    this.Verify(sender, row);
  }

  public void Verify(PXCache sender, PMWorkCodeCostCodeRange row)
  {
    if (string.IsNullOrEmpty(row.CostCodeFrom) && string.IsNullOrEmpty(row.CostCodeTo))
      sender.RaiseExceptionHandling(this._FieldName, (object) row, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("{0} or {1} must have a value.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName(sender, "costCodeFrom"),
        (object) PXUIFieldAttribute.GetDisplayName(sender, "costCodeTo")
      }), (PXErrorLevel) 5));
    else if (string.IsNullOrEmpty(row.CostCodeFrom))
    {
      if (PXSelectBase<PMWorkCodeCostCodeRange, PXViewOf<PMWorkCodeCostCodeRange>.BasedOn<SelectFromBase<PMWorkCodeCostCodeRange, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.workCodeID, NotEqual<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeFrom, LessEqual<P.AsString>>>>>.Or<BqlOperand<PMWorkCodeCostCodeRange.costCodeFrom, IBqlString>.IsNull>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) row.WorkCodeID,
        (object) row.CostCodeTo
      }).TopFirst == null)
        return;
      sender.RaiseExceptionHandling("costCodeFrom", (object) row, (object) null, (Exception) new PXSetPropertyException("One cost code cannot be associated with multiple workers' compensation codes."));
    }
    else if (string.IsNullOrEmpty(row.CostCodeTo))
    {
      if (PXSelectBase<PMWorkCodeCostCodeRange, PXViewOf<PMWorkCodeCostCodeRange>.BasedOn<SelectFromBase<PMWorkCodeCostCodeRange, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.workCodeID, NotEqual<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeTo, GreaterEqual<P.AsString>>>>>.Or<BqlOperand<PMWorkCodeCostCodeRange.costCodeTo, IBqlString>.IsNull>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) row.WorkCodeID,
        (object) row.CostCodeFrom
      }).TopFirst == null)
        return;
      sender.RaiseExceptionHandling("costCodeTo", (object) row, (object) null, (Exception) new PXSetPropertyException("One cost code cannot be associated with multiple workers' compensation codes."));
    }
    else if (row.CostCodeTo.CompareTo(row.CostCodeFrom) < 0)
    {
      sender.RaiseExceptionHandling("costCodeTo", (object) row, (object) row.CostCodeTo, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("The value for {0} must be equal to or greater than the value for {1}.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName(sender, "costCodeTo"),
        (object) PXUIFieldAttribute.GetDisplayName(sender, "costCodeFrom")
      })));
    }
    else
    {
      if (PXSelectBase<PMWorkCodeCostCodeRange, PXViewOf<PMWorkCodeCostCodeRange>.BasedOn<SelectFromBase<PMWorkCodeCostCodeRange, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.workCodeID, NotEqual<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeFrom, LessEqual<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeTo, IsNull>>>>.Or<BqlOperand<PMWorkCodeCostCodeRange.costCodeTo, IBqlString>.IsGreaterEqual<P.AsString>>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeTo, GreaterEqual<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWorkCodeCostCodeRange.costCodeFrom, IsNull>>>>.Or<BqlOperand<PMWorkCodeCostCodeRange.costCodeFrom, IBqlString>.IsLessEqual<P.AsString>>>>>>>.Config>.Select(sender.Graph, new object[5]
      {
        (object) row.WorkCodeID,
        (object) row.CostCodeTo,
        (object) row.CostCodeFrom,
        (object) row.CostCodeFrom,
        (object) row.CostCodeTo
      }).TopFirst == null)
        return;
      sender.RaiseExceptionHandling(this._FieldName, (object) row, (object) row.CostCodeFrom, (Exception) new PXSetPropertyException("One cost code cannot be associated with multiple workers' compensation codes.", (PXErrorLevel) 5));
    }
  }
}
