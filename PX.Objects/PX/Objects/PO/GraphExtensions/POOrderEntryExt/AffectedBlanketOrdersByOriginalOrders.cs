// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.AffectedBlanketOrdersByOriginalOrders
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class AffectedBlanketOrdersByOriginalOrders : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedBlanketOrdersByOriginalOrders, POOrderEntry, POOrderEntry.POOrderR, POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  protected override bool ClearAffectedCaches => false;

  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(POOrderEntry.POOrderR entity)
  {
    if (entity.OrderType != "BL")
      return false;
    PXCache cache = ((PXSelectBase) this.Base.poOrder).Cache;
    int? valueOriginal1 = (int?) cache.GetValueOriginal<POOrderEntry.POOrderR.linesToCompleteCntr>((object) entity);
    int? valueOriginal2 = (int?) cache.GetValueOriginal<POOrderEntry.POOrderR.linesToCloseCntr>((object) entity);
    if (object.Equals((object) valueOriginal1, (object) entity.LinesToCompleteCntr) && object.Equals((object) valueOriginal2, (object) entity.LinesToCloseCntr))
      return false;
    int? nullable = valueOriginal1;
    int num1 = 0;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      nullable = entity.LinesToCompleteCntr;
      int num2 = 0;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        nullable = valueOriginal2;
        int num3 = 0;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
        {
          nullable = entity.LinesToCloseCntr;
          int num4 = 0;
          return nullable.GetValueOrDefault() == num4 & nullable.HasValue;
        }
      }
    }
    return true;
  }

  protected override void ProcessAffectedEntity(
    POOrderEntry primaryGraph,
    POOrderEntry.POOrderR entity)
  {
    POOrder order = POOrder.PK.Find((PXGraph) primaryGraph, entity.OrderType, entity.OrderNbr);
    primaryGraph.UpdateDocumentState(order);
  }

  protected override POOrderEntry.POOrderR ActualizeEntity(
    POOrderEntry primaryGraph,
    POOrderEntry.POOrderR entity)
  {
    return PXResultset<POOrderEntry.POOrderR>.op_Implicit(PXSelectBase<POOrderEntry.POOrderR, PXSelect<POOrderEntry.POOrderR, Where<POOrderEntry.POOrderR.orderType, Equal<Required<POOrderEntry.POOrderR.orderType>>, And<POOrderEntry.POOrderR.orderNbr, Equal<Required<POOrderEntry.POOrderR.orderNbr>>>>>.Config>.Select((PXGraph) primaryGraph, new object[2]
    {
      (object) entity.OrderType,
      (object) entity.OrderNbr
    }));
  }

  protected virtual bool IsBlanketChildRow(POLine row)
  {
    return POOrderType.IsUseBlanket(row?.OrderType) && row?.POType == "BL" && row.PONbr != null && row.POLineNbr.HasValue;
  }

  protected virtual POLineR FindBlanketRow(PXCache rowCache, POLine normalRow)
  {
    return PXParentAttribute.SelectParent<POLineR>(rowCache, (object) normalRow);
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<BqlOperand<POLine.cancelled, IBqlBool>.IsEqual<True>>, decimal0, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.completed, Equal<True>>>>>.And<BqlOperand<POLine.lineType, IBqlString>.IsNotEqual<POLineType.service>>>, POLine.baseReceivedQty>>, POLine.baseOrderQty>), typeof (SumCalc<POLineR.baseOrderedQty>))]
  protected virtual void _(Events.CacheAttached<POLine.completed> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<POLineR.completed, Equal<True>, Or<POLineR.cancelled, Equal<True>>>, decimal0>, Maximum<Sub<POLineR.orderQty, POLineR.orderedQty>, decimal0>>), typeof (SumCalc<POOrderEntry.POOrderR.openOrderQty>))]
  protected virtual void _(Events.CacheAttached<POLineR.openQty> e)
  {
  }

  protected virtual void _(Events.RowDeleting<POLineR> e)
  {
    ((Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<POLineR>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 0);
    e.Cancel = true;
  }

  protected virtual void _(Events.RowInserting<POLineR> e) => e.Cancel = true;

  protected virtual void _(Events.FieldVerifying<POLine, POLine.orderQty> e)
  {
    if (!(e.Row.OrderType == "BL"))
      return;
    Decimal? newValue = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<POLine, POLine.orderQty>, POLine, object>) e).NewValue;
    Decimal? orderedQty = e.Row.OrderedQty;
    if (newValue.GetValueOrDefault() < orderedQty.GetValueOrDefault() & newValue.HasValue & orderedQty.HasValue)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<POLine, POLine.orderQty>>) e).Cancel = true;
      throw new PXSetPropertyException("The quantity in the Order Qty. column cannot be less than the quantity in the Qty. On Orders column.");
    }
  }

  protected virtual void _(Events.RowSelected<POLine> e)
  {
    if (!this.IsBlanketChildRow(e.Row))
      return;
    this.RaiseNormalOrderQtyExceedsWarning(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<POLine>>) e).Cache, e.Row, this.FindBlanketRow(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<POLine>>) e).Cache, e.Row) ?? throw new PXArgumentException("blanketRow"));
  }

  protected virtual void RaiseNormalOrderQtyExceedsWarning(
    PXCache normalRowCache,
    POLine normalRow,
    POLineR blanketRow)
  {
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<POLine.orderQty>(normalRowCache, (object) normalRow)))
      return;
    Decimal? nullable1 = normalRow.OrderQty;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
    {
      bool? nullable2 = normalRow.Completed;
      bool flag1 = false;
      if (nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue)
      {
        nullable2 = normalRow.Cancelled;
        bool flag2 = false;
        if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
        {
          nullable1 = blanketRow.OrderedQty;
          Decimal? orderQty = blanketRow.OrderQty;
          if (nullable1.GetValueOrDefault() > orderQty.GetValueOrDefault() & nullable1.HasValue & orderQty.HasValue)
          {
            PXSetPropertyException propertyException = new PXSetPropertyException("The order quantity of the line exceeds the quantity in the Blanket Open Qty. column of the linked line in the {0} blanket purchase order.", (PXErrorLevel) 2, new object[1]
            {
              (object) normalRow.PONbr
            });
            normalRowCache.RaiseExceptionHandling<POLine.orderQty>((object) normalRow, (object) normalRow.OrderQty, (Exception) propertyException);
            return;
          }
        }
      }
    }
    normalRowCache.RaiseExceptionHandling<POLine.orderQty>((object) normalRow, (object) normalRow.OrderQty, (Exception) null);
  }

  protected virtual void _(Events.RowUpdated<POLine> e)
  {
    if (!this.IsBlanketChildRow(e.Row))
      return;
    if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache.ObjectsEqual<POLine.completed, POLine.closed>((object) e.Row, (object) e.OldRow))
    {
      POLineR blanketRow1 = this.FindBlanketRow(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache, e.Row);
      bool? nullable1 = blanketRow1 != null ? blanketRow1.Completed : throw new PXArgumentException("blanketRow");
      bool? closed1 = blanketRow1.Closed;
      PXCache cache = ((PXSelectBase) this.Base.poLiner).Cache;
      POLineR blanketRow2 = this.CompleteBlanketRow(cache, blanketRow1, e.Row);
      POLineR poLineR = this.CloseBlanketRow(cache, blanketRow2, e.Row);
      bool? nullable2 = nullable1;
      bool? nullable3 = poLineR.Completed;
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      {
        nullable3 = closed1;
        bool? closed2 = poLineR.Closed;
        if (nullable3.GetValueOrDefault() == closed2.GetValueOrDefault() & nullable3.HasValue == closed2.HasValue)
          goto label_7;
      }
      cache.Update((object) poLineR);
      ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache.Current = (object) e.Row;
    }
label_7:
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache.ObjectsEqual<POLine.orderQty>((object) e.Row, (object) e.OldRow))
      return;
    this.RaiseNormalOrderQtyExceedsWarning(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache, e.Row, this.FindBlanketRow(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache, e.Row) ?? throw new PXArgumentException("blanketRow"));
  }

  /// the same logic for the <see cref="T:PX.Objects.PO.POLine" />
  /// <see cref="M:PX.Objects.PO.GraphExtensions.AffectedPOOrdersByPOLine`2.CompleteBlanketRow(PX.Data.PXCache,PX.Objects.PO.POLine,PX.Objects.PO.POLine,PX.Objects.PO.POLine)" />
  protected virtual POLineR CompleteBlanketRow(
    PXCache blanketRowCache,
    POLineR blanketRow,
    POLine normalRow)
  {
    bool? completed = normalRow.Completed;
    bool flag1 = false;
    bool flag2;
    if (completed.GetValueOrDefault() == flag1 & completed.HasValue)
    {
      flag2 = false;
    }
    else
    {
      if (blanketRow.CompletePOLine == "Q")
      {
        if (POLineType.IsService(blanketRow.LineType))
        {
          Decimal? billedQty = blanketRow.BilledQty;
          Decimal? orderQty = blanketRow.OrderQty;
          Decimal? nullable1 = blanketRow.RcptQtyThreshold;
          Decimal? nullable2 = orderQty.HasValue & nullable1.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
          Decimal num = (Decimal) 100;
          Decimal? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new Decimal?(nullable2.GetValueOrDefault() / num);
          Decimal? nullable4 = nullable3;
          flag2 = billedQty.GetValueOrDefault() >= nullable4.GetValueOrDefault() & billedQty.HasValue & nullable4.HasValue;
        }
        else
        {
          Decimal? receivedQty = blanketRow.ReceivedQty;
          Decimal? orderQty = blanketRow.OrderQty;
          Decimal? nullable5 = blanketRow.RcptQtyThreshold;
          Decimal? nullable6 = orderQty.HasValue & nullable5.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
          Decimal num = (Decimal) 100;
          Decimal? nullable7;
          if (!nullable6.HasValue)
          {
            nullable5 = new Decimal?();
            nullable7 = nullable5;
          }
          else
            nullable7 = new Decimal?(nullable6.GetValueOrDefault() / num);
          Decimal? nullable8 = nullable7;
          flag2 = receivedQty.GetValueOrDefault() >= nullable8.GetValueOrDefault() & receivedQty.HasValue & nullable8.HasValue;
        }
      }
      else
      {
        Decimal? billedAmt = blanketRow.BilledAmt;
        Decimal? extCost = blanketRow.ExtCost;
        Decimal? nullable9 = blanketRow.RetainageAmt;
        Decimal? nullable10 = extCost.HasValue & nullable9.HasValue ? new Decimal?(extCost.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable11 = blanketRow.RcptQtyThreshold;
        Decimal? nullable12;
        if (!(nullable10.HasValue & nullable11.HasValue))
        {
          nullable9 = new Decimal?();
          nullable12 = nullable9;
        }
        else
          nullable12 = new Decimal?(nullable10.GetValueOrDefault() * nullable11.GetValueOrDefault());
        Decimal? nullable13 = nullable12;
        Decimal num = (Decimal) 100;
        Decimal? nullable14;
        if (!nullable13.HasValue)
        {
          nullable11 = new Decimal?();
          nullable14 = nullable11;
        }
        else
          nullable14 = new Decimal?(nullable13.GetValueOrDefault() / num);
        Decimal? nullable15 = nullable14;
        flag2 = billedAmt.GetValueOrDefault() >= nullable15.GetValueOrDefault() & billedAmt.HasValue & nullable15.HasValue;
      }
      if (flag2)
        flag2 = PXResultset<POLineR>.op_Implicit(PXSelectBase<POLineR, PXViewOf<POLineR>.BasedOn<SelectFromBase<POLineR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POLineR.pOType>.IsRelatedTo<POLineR.orderType>, Field<POLineR.pONbr>.IsRelatedTo<POLineR.orderNbr>, Field<POLineR.pOLineNbr>.IsRelatedTo<POLineR.lineNbr>>.WithTablesOf<POLineR, POLineR>, POLineR, POLineR>.SameAsCurrent>, And<BqlOperand<POLineR.completed, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLineR.orderType, NotEqual<P.AsString.ASCII>>>>, Or<BqlOperand<POLineR.orderNbr, IBqlString>.IsNotEqual<P.AsString>>>>.Or<BqlOperand<POLineR.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new POLineR[1]
        {
          blanketRow
        }, new object[3]
        {
          (object) normalRow.OrderType,
          (object) normalRow.OrderNbr,
          (object) normalRow.LineNbr
        })) == null;
    }
    blanketRow.Completed = new bool?(flag2);
    return blanketRow;
  }

  /// similar for the <see cref="T:PX.Objects.PO.POLine" />
  /// <see cref="M:PX.Objects.PO.GraphExtensions.AffectedPOOrdersByPOLine`2.CloseBlanketRow(PX.Data.PXCache,PX.Objects.PO.POLine,PX.Objects.PO.POLine,PX.Objects.PO.POLine)" />
  protected virtual POLineR CloseBlanketRow(
    PXCache blanketRowCache,
    POLineR blanketRow,
    POLine normalRow)
  {
    bool? completed = blanketRow.Completed;
    bool flag1 = false;
    bool flag2;
    if (completed.GetValueOrDefault() == flag1 & completed.HasValue)
    {
      flag2 = false;
    }
    else
    {
      bool? closed = normalRow.Closed;
      bool flag3 = false;
      if (closed.GetValueOrDefault() == flag3 & closed.HasValue)
        flag2 = false;
      else
        flag2 = PXResultset<POLineR>.op_Implicit(PXSelectBase<POLineR, PXViewOf<POLineR>.BasedOn<SelectFromBase<POLineR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POLineR.pOType>.IsRelatedTo<POLineR.orderType>, Field<POLineR.pONbr>.IsRelatedTo<POLineR.orderNbr>, Field<POLineR.pOLineNbr>.IsRelatedTo<POLineR.lineNbr>>.WithTablesOf<POLineR, POLineR>, POLineR, POLineR>.SameAsCurrent>, And<BqlOperand<POLineR.closed, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLineR.orderType, NotEqual<P.AsString.ASCII>>>>, Or<BqlOperand<POLineR.orderNbr, IBqlString>.IsNotEqual<P.AsString>>>>.Or<BqlOperand<POLineR.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new POLineR[1]
        {
          blanketRow
        }, new object[3]
        {
          (object) normalRow.OrderType,
          (object) normalRow.OrderNbr,
          (object) normalRow.LineNbr
        })) == null;
    }
    blanketRow.Closed = new bool?(flag2);
    return blanketRow;
  }
}
