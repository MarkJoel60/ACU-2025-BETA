// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.AffectedPOOrdersByPOLine`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions;

public abstract class AffectedPOOrdersByPOLine<TSelf, TGraph> : 
  ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, POOrder, POOrderEntry>
  where TSelf : AffectedPOOrdersByPOLine<TSelf, TGraph>
  where TGraph : PXGraph
{
  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(POOrder entity)
  {
    PXCache<POOrder> pxCache = GraphHelper.Caches<POOrder>((PXGraph) this.Base);
    int? valueOriginal1 = (int?) ((PXCache) pxCache).GetValueOriginal<POOrder.linesToCloseCntr>((object) entity);
    int? valueOriginal2 = (int?) ((PXCache) pxCache).GetValueOriginal<POOrder.linesToCompleteCntr>((object) entity);
    if (object.Equals((object) valueOriginal1, (object) entity.LinesToCloseCntr) && object.Equals((object) valueOriginal2, (object) entity.LinesToCompleteCntr))
      return false;
    int? nullable = valueOriginal1;
    int num1 = 0;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      nullable = valueOriginal2;
      int num2 = 0;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        nullable = entity.LinesToCloseCntr;
        int num3 = 0;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
        {
          nullable = entity.LinesToCompleteCntr;
          int num4 = 0;
          return nullable.GetValueOrDefault() == num4 & nullable.HasValue;
        }
      }
    }
    return true;
  }

  protected override IEnumerable<POOrder> GetAffectedEntities()
  {
    return base.GetAffectedEntities().BeginWith<POOrder>((Func<POOrder, bool>) (x => x.OrderType != "BL"));
  }

  protected override void ProcessAffectedEntity(POOrderEntry primaryGraph, POOrder entity)
  {
    primaryGraph.UpdateDocumentState(entity);
  }

  protected override POOrder ActualizeEntity(POOrderEntry primaryGraph, POOrder entity)
  {
    return PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXSelect<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>.Config>.Select((PXGraph) primaryGraph, new object[2]
    {
      (object) entity.OrderType,
      (object) entity.OrderNbr
    }));
  }

  [PXMergeAttributes]
  [PXParent(typeof (SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderType, Equal<POOrderType.blanket>>>>, And<BqlOperand<POLine.orderType, IBqlString>.IsEqual<BqlField<POLine.pOType, IBqlString>.FromCurrent>>>, And<BqlOperand<POLine.orderNbr, IBqlString>.IsEqual<BqlField<POLine.pONbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<POLine.lineNbr, IBqlInt>.IsEqual<BqlField<POLine.pOLineNbr, IBqlInt>.FromCurrent>>>))]
  protected virtual void _(Events.CacheAttached<POLine.pOLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<BqlOperand<POLine.cancelled, IBqlBool>.IsEqual<True>>, decimal0, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.completed, Equal<True>>>>>.And<BqlOperand<POLine.lineType, IBqlString>.IsNotEqual<POLineType.service>>>, POLine.baseReceivedQty>>, POLine.baseOrderQty>), typeof (SumCalc<POLine.baseOrderedQty>))]
  protected virtual void _(Events.CacheAttached<POLine.completed> e)
  {
  }

  protected virtual void _(Events.RowUpdated<POLine> e)
  {
    if (e.Row?.POType != "BL" || this.UpdateBlanketRow(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache, e.Row, e.OldRow, false) == null)
      return;
    ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<POLine>>) e).Cache.Current = (object) e.Row;
  }

  protected virtual POLine UpdateBlanketRow(
    PXCache cache,
    POLine normalRow,
    POLine normalOldRow,
    bool hardCheck)
  {
    if (!hardCheck && cache.ObjectsEqual<POLine.completed, POLine.closed>((object) normalRow, (object) normalOldRow))
      return (POLine) null;
    POLine blanketRow = this.FindBlanketRow(cache, normalRow);
    bool? nullable1 = blanketRow != null ? blanketRow.Completed : throw new PXArgumentException("blanketRow");
    bool? closed = blanketRow.Closed;
    bool? nullable2;
    bool? nullable3;
    if (!hardCheck)
    {
      nullable2 = normalRow.Completed;
      nullable3 = normalOldRow.Completed;
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        goto label_7;
    }
    blanketRow = this.CompleteBlanketRow(cache, blanketRow, normalRow, normalOldRow);
label_7:
    if (!hardCheck)
    {
      nullable3 = nullable1;
      nullable2 = blanketRow.Completed;
      if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
      {
        nullable2 = normalRow.Closed;
        nullable3 = normalOldRow.Closed;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
          goto label_11;
      }
    }
    blanketRow = this.CloseBlanketRow(cache, blanketRow, normalRow, normalOldRow);
label_11:
    nullable3 = nullable1;
    nullable2 = blanketRow.Completed;
    if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
    {
      nullable2 = closed;
      nullable3 = blanketRow.Closed;
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        return (POLine) null;
    }
    return (POLine) cache.Update((object) blanketRow);
  }

  protected POLine FindBlanketRow(PXCache rowCache, POLine normalRow)
  {
    return PXParentAttribute.SelectParent<POLine>(rowCache, (object) normalRow);
  }

  protected virtual POLine CompleteBlanketRow(
    PXCache cache,
    POLine blanketRow,
    POLine normalRow,
    POLine normalOldRow)
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
          Decimal? baseBilledQty = blanketRow.BaseBilledQty;
          Decimal? baseOrderQty = blanketRow.BaseOrderQty;
          Decimal? nullable1 = blanketRow.RcptQtyThreshold;
          Decimal? nullable2 = baseOrderQty.HasValue & nullable1.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
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
          flag2 = baseBilledQty.GetValueOrDefault() >= nullable4.GetValueOrDefault() & baseBilledQty.HasValue & nullable4.HasValue;
        }
        else
        {
          Decimal? baseReceivedQty = blanketRow.BaseReceivedQty;
          Decimal? baseOrderQty = blanketRow.BaseOrderQty;
          Decimal? nullable5 = blanketRow.RcptQtyThreshold;
          Decimal? nullable6 = baseOrderQty.HasValue & nullable5.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
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
          flag2 = baseReceivedQty.GetValueOrDefault() >= nullable8.GetValueOrDefault() & baseReceivedQty.HasValue & nullable8.HasValue;
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
        flag2 = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POLine.pOType>.IsRelatedTo<POLine.orderType>, Field<POLine.pONbr>.IsRelatedTo<POLine.orderNbr>, Field<POLine.pOLineNbr>.IsRelatedTo<POLine.lineNbr>>.WithTablesOf<POLine, POLine>, POLine, POLine>.SameAsCurrent>, And<BqlOperand<POLine.completed, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderType, NotEqual<P.AsString.ASCII>>>>, Or<BqlOperand<POLine.orderNbr, IBqlString>.IsNotEqual<P.AsString>>>>.Or<BqlOperand<POLine.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new POLine[1]
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

  protected virtual POLine CloseBlanketRow(
    PXCache cache,
    POLine blanketRow,
    POLine normalRow,
    POLine normalOldRow)
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
        flag2 = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POLine.pOType>.IsRelatedTo<POLine.orderType>, Field<POLine.pONbr>.IsRelatedTo<POLine.orderNbr>, Field<POLine.pOLineNbr>.IsRelatedTo<POLine.lineNbr>>.WithTablesOf<POLine, POLine>, POLine, POLine>.SameAsCurrent>, And<BqlOperand<POLine.closed, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderType, NotEqual<P.AsString.ASCII>>>>, Or<BqlOperand<POLine.orderNbr, IBqlString>.IsNotEqual<P.AsString>>>>.Or<BqlOperand<POLine.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new POLine[1]
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
