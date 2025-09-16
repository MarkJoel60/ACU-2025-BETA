// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.BaseUpdatePOAccrual`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO.GraphExtensions;

public abstract class BaseUpdatePOAccrual<TMultiCurrencyExtension, TGraph, TPrimaryDAC> : 
  PXGraphExtension<
  #nullable disable
  TMultiCurrencyExtension, TGraph>
  where TMultiCurrencyExtension : MultiCurrencyGraph<TGraph, TPrimaryDAC>
  where TGraph : PXGraph
  where TPrimaryDAC : class, IBqlTable, new()
{
  [PXMergeAttributes]
  [POUnbilledTaxR(typeof (PX.Objects.PO.POOrder), typeof (POTax), typeof (POTaxTran), Inventory = typeof (PX.Objects.PO.POLine.inventoryID), UOM = typeof (PX.Objects.PO.POLine.uOM), LineQty = typeof (PX.Objects.PO.POLine.unbilledQty))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.taxCategoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBQuantityAttribute))]
  [PXDBQuantity]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.orderedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXDBBaseQtyWithOrigQty(typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.orderedQty), typeof (PX.Objects.PO.POLine.uOM), typeof (PX.Objects.PO.POLine.baseOrderQty), typeof (PX.Objects.PO.POLine.orderQty), HandleEmptyKey = true, MinValue = 0.0)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.baseOrderedQty> e)
  {
  }

  [PXMergeAttributes]
  [POCommitment]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.commitmentID> e)
  {
  }

  public virtual POAccrualRecord GetAccrualStatusSummary(PX.Objects.PO.POLine poLine)
  {
    string orderType = poLine.OrderType;
    string orderNbr = poLine.OrderNbr;
    int? lineNbr = poLine.LineNbr;
    if (!EnumerableExtensions.IsIn<string>(poLine.POAccrualType, "O", "R"))
      return new POAccrualRecord();
    return this.Accumulate((IEnumerable<POAccrualRecord>) GraphHelper.RowCast<POAccrualStatus>((IEnumerable) PXSelectBase<POAccrualStatus, PXSelect<POAccrualStatus, Where<POAccrualStatus.orderType, Equal<Required<POAccrualStatus.orderType>>, And<POAccrualStatus.orderNbr, Equal<Required<POAccrualStatus.orderNbr>>, And<POAccrualStatus.orderLineNbr, Equal<Required<POAccrualStatus.orderLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[3]
    {
      (object) orderType,
      (object) orderNbr,
      (object) lineNbr
    })).Select<POAccrualStatus, POAccrualRecord>(new Func<POAccrualStatus, POAccrualRecord>(POAccrualRecord.FromPOAccrualStatus)).ToList<POAccrualRecord>());
  }

  protected virtual POAccrualRecord Accumulate(IEnumerable<POAccrualRecord> records)
  {
    POAccrualRecord poAccrualRecord1 = new POAccrualRecord();
    foreach (POAccrualRecord record in records)
    {
      Decimal? nullable1 = poAccrualRecord1.ReceivedQty;
      int num1;
      if (nullable1.HasValue)
      {
        nullable1 = record.ReceivedQty;
        if (nullable1.HasValue)
        {
          num1 = poAccrualRecord1.ReceivedUOM == null || record.ReceivedUOM == null ? 0 : (!string.Equals(poAccrualRecord1.ReceivedUOM, record.ReceivedUOM, StringComparison.OrdinalIgnoreCase) ? 1 : 0);
          goto label_6;
        }
      }
      num1 = 1;
label_6:
      Decimal? nullable2;
      if (num1 != 0)
      {
        poAccrualRecord1.ReceivedUOM = (string) null;
        POAccrualRecord poAccrualRecord2 = poAccrualRecord1;
        nullable1 = new Decimal?();
        Decimal? nullable3 = nullable1;
        poAccrualRecord2.ReceivedQty = nullable3;
      }
      else
      {
        nullable1 = record.ReceivedQty;
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          poAccrualRecord1.ReceivedUOM = record.ReceivedUOM;
          POAccrualRecord poAccrualRecord3 = poAccrualRecord1;
          nullable1 = poAccrualRecord3.ReceivedQty;
          nullable2 = record.ReceivedQty;
          poAccrualRecord3.ReceivedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
      }
      POAccrualRecord poAccrualRecord4 = poAccrualRecord1;
      nullable2 = poAccrualRecord4.BaseReceivedQty;
      nullable1 = record.BaseReceivedQty;
      poAccrualRecord4.BaseReceivedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      POAccrualRecord poAccrualRecord5 = poAccrualRecord1;
      nullable1 = poAccrualRecord5.ReceivedCost;
      nullable2 = record.ReceivedCost;
      poAccrualRecord5.ReceivedCost = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      nullable2 = poAccrualRecord1.BilledQty;
      int num3;
      if (nullable2.HasValue)
      {
        nullable2 = record.BilledQty;
        if (nullable2.HasValue)
        {
          num3 = poAccrualRecord1.BilledUOM == null || record.BilledUOM == null ? 0 : (!string.Equals(poAccrualRecord1.BilledUOM, record.BilledUOM, StringComparison.OrdinalIgnoreCase) ? 1 : 0);
          goto label_14;
        }
      }
      num3 = 1;
label_14:
      if (num3 != 0)
      {
        poAccrualRecord1.BilledUOM = (string) null;
        POAccrualRecord poAccrualRecord6 = poAccrualRecord1;
        nullable2 = new Decimal?();
        Decimal? nullable4 = nullable2;
        poAccrualRecord6.BilledQty = nullable4;
      }
      else
      {
        nullable2 = record.BilledQty;
        Decimal num4 = 0M;
        if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
        {
          poAccrualRecord1.BilledUOM = record.BilledUOM;
          POAccrualRecord poAccrualRecord7 = poAccrualRecord1;
          nullable2 = poAccrualRecord7.BilledQty;
          nullable1 = record.BilledQty;
          poAccrualRecord7.BilledQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        }
      }
      POAccrualRecord poAccrualRecord8 = poAccrualRecord1;
      nullable1 = poAccrualRecord8.BaseBilledQty;
      nullable2 = record.BaseBilledQty;
      poAccrualRecord8.BaseBilledQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      nullable2 = poAccrualRecord1.CuryBilledAmt;
      int num5;
      if (nullable2.HasValue)
      {
        nullable2 = record.CuryBilledAmt;
        if (nullable2.HasValue)
        {
          num5 = poAccrualRecord1.BillCuryID == null || record.BillCuryID == null ? 0 : (!string.Equals(poAccrualRecord1.BillCuryID, record.BillCuryID, StringComparison.OrdinalIgnoreCase) ? 1 : 0);
          goto label_22;
        }
      }
      num5 = 1;
label_22:
      if (num5 != 0)
      {
        poAccrualRecord1.BillCuryID = (string) null;
        POAccrualRecord poAccrualRecord9 = poAccrualRecord1;
        nullable2 = new Decimal?();
        Decimal? nullable5 = nullable2;
        poAccrualRecord9.CuryBilledAmt = nullable5;
        POAccrualRecord poAccrualRecord10 = poAccrualRecord1;
        nullable2 = new Decimal?();
        Decimal? nullable6 = nullable2;
        poAccrualRecord10.CuryBilledCost = nullable6;
        POAccrualRecord poAccrualRecord11 = poAccrualRecord1;
        nullable2 = new Decimal?();
        Decimal? nullable7 = nullable2;
        poAccrualRecord11.CuryBilledDiscAmt = nullable7;
      }
      else
      {
        nullable2 = record.CuryBilledAmt;
        Decimal num6 = 0M;
        if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue))
        {
          poAccrualRecord1.BillCuryID = record.BillCuryID;
          POAccrualRecord poAccrualRecord12 = poAccrualRecord1;
          nullable2 = poAccrualRecord12.CuryBilledAmt;
          nullable1 = record.CuryBilledAmt;
          poAccrualRecord12.CuryBilledAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          POAccrualRecord poAccrualRecord13 = poAccrualRecord1;
          nullable1 = poAccrualRecord13.CuryBilledCost;
          nullable2 = record.CuryBilledCost;
          poAccrualRecord13.CuryBilledCost = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          POAccrualRecord poAccrualRecord14 = poAccrualRecord1;
          nullable2 = poAccrualRecord14.CuryBilledDiscAmt;
          nullable1 = record.CuryBilledDiscAmt;
          poAccrualRecord14.CuryBilledDiscAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        }
      }
      POAccrualRecord poAccrualRecord15 = poAccrualRecord1;
      nullable1 = poAccrualRecord15.BilledAmt;
      nullable2 = record.BilledAmt;
      poAccrualRecord15.BilledAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      POAccrualRecord poAccrualRecord16 = poAccrualRecord1;
      nullable2 = poAccrualRecord16.BilledCost;
      nullable1 = record.BilledCost;
      poAccrualRecord16.BilledCost = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      POAccrualRecord poAccrualRecord17 = poAccrualRecord1;
      nullable1 = poAccrualRecord17.BilledDiscAmt;
      nullable2 = record.BilledDiscAmt;
      poAccrualRecord17.BilledDiscAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      POAccrualRecord poAccrualRecord18 = poAccrualRecord1;
      nullable2 = poAccrualRecord18.PPVAmt;
      nullable1 = record.PPVAmt;
      poAccrualRecord18.PPVAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    return poAccrualRecord1;
  }

  protected void SetIfNotNull<TField>(PXCache cache, POAccrualStatus row, object value) where TField : IBqlField
  {
    if (value == null)
      return;
    cache.SetValue<TField>((object) row, value);
  }

  protected void SetIfNotEmpty<TField>(PXCache cache, POAccrualStatus row, Decimal? value) where TField : IBqlField
  {
    if (!value.HasValue)
      return;
    Decimal? nullable = value;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    cache.SetValue<TField>((object) row, (object) value);
  }

  public virtual PPVCalculationResult CalculatePPV(
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.AP.APTran tran,
    POAccrualStatus poAccrual,
    Action<OnAccrualSplitDefinedArgs> onAccrualSplitDefined = null)
  {
    List<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>> source = this.CollectReceiptLinesForBilling(apdoc, tran, poAccrual);
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    if (tran.POAccrualType == "R")
    {
      PX.Objects.PO.POReceiptLine rctLine = source.First<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>().Item1;
      nullable1 = rctLine.TranCostFinal;
      if (!nullable1.HasValue)
      {
        if (POLineType.UsePOAccrual(rctLine.LineType))
          throw new PXException("IN Issue for the Purchase Return must be released prior to billing.");
      }
      else
      {
        nullable1 = poAccrual.ReceivedCost;
        Decimal apSign1 = this.GetAPSign(rctLine);
        nullable2 = rctLine.TranCostFinal;
        nullable3 = nullable2.HasValue ? new Decimal?(apSign1 * nullable2.GetValueOrDefault()) : new Decimal?();
        if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        {
          POAccrualStatus poAccrualStatus = poAccrual;
          Decimal apSign2 = this.GetAPSign(rctLine);
          nullable3 = rctLine.TranCostFinal;
          Decimal? nullable4;
          if (!nullable3.HasValue)
          {
            nullable1 = new Decimal?();
            nullable4 = nullable1;
          }
          else
            nullable4 = new Decimal?(apSign2 * nullable3.GetValueOrDefault());
          poAccrualStatus.ReceivedCost = nullable4;
        }
      }
    }
    Decimal sign1 = tran.Sign;
    if (source.Any<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>((Func<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>, bool>) (r => this.GetAPSign(r.Item1) < 0M)))
    {
      if (tran.POAccrualType == "O")
        throw new InvalidOperationException("PO Return cannot be Order-based, it must create its own separate PO Accrual.");
      sign1 *= -1M;
    }
    bool flag1 = source.All<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>((Func<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>, bool>) (rl => string.Equals(rl.Item1.UOM, tran.UOM, StringComparison.OrdinalIgnoreCase)));
    Decimal? nullable5;
    bool flag2;
    if (flag1)
    {
      nullable3 = poAccrual.ReceivedQty;
      if (nullable3.HasValue && EnumerableExtensions.IsIn<string>(poAccrual.ReceivedUOM, (string) null, tran.UOM))
      {
        nullable3 = poAccrual.BilledQty;
        if (nullable3.HasValue && EnumerableExtensions.IsIn<string>(poAccrual.BilledUOM, (string) null, tran.UOM))
        {
          nullable3 = poAccrual.BilledQty;
          Decimal sign2 = tran.Sign;
          nullable2 = tran.Qty;
          nullable1 = nullable2.HasValue ? new Decimal?(sign2 * nullable2.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable6;
          if (!(nullable3.HasValue & nullable1.HasValue))
          {
            nullable2 = new Decimal?();
            nullable6 = nullable2;
          }
          else
            nullable6 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
          nullable5 = nullable6;
          nullable1 = poAccrual.ReceivedQty;
          nullable3 = nullable5;
          flag2 = nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue;
          goto label_24;
        }
      }
    }
    nullable3 = poAccrual.BaseBilledQty;
    Decimal sign3 = tran.Sign;
    nullable2 = tran.BaseQty;
    nullable1 = nullable2.HasValue ? new Decimal?(sign3 * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    nullable5 = nullable7;
    nullable1 = poAccrual.BaseReceivedQty;
    nullable3 = nullable5;
    flag2 = nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue;
label_24:
    nullable3 = flag1 ? tran.Qty : tran.BaseQty;
    Decimal num1 = sign1;
    Decimal? nullable8;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable8 = nullable1;
    }
    else
      nullable8 = new Decimal?(nullable3.GetValueOrDefault() * num1);
    Decimal? nullable9 = nullable8;
    Decimal? nullable10 = this.GetExpensePostingAmount((PXGraph) ((PXGraphExtension<TGraph>) this).Base, tran, apdoc).Base;
    nullable3 = nullable10;
    Decimal num2 = sign1;
    Decimal? nullable11;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable11 = nullable1;
    }
    else
      nullable11 = new Decimal?(nullable3.GetValueOrDefault() * num2);
    Decimal? nullable12 = nullable11;
    Decimal? nullable13 = new Decimal?(0M);
    foreach (Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit> tuple in source)
    {
      nullable3 = nullable9;
      Decimal num3 = 0M;
      if (!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue))
      {
        PX.Objects.PO.POReceiptLine rctLine = tuple.Item1;
        POAccrualSplit poAccrualSplit = tuple.Item2;
        Decimal? nullable14 = flag1 ? rctLine.ReceiptQty : rctLine.BaseReceiptQty;
        Decimal? nullable15;
        Decimal? nullable16;
        if (poAccrualSplit != null)
        {
          nullable3 = flag1 ? poAccrualSplit.AccruedQty : poAccrualSplit.BaseAccruedQty;
          Decimal? nullable17;
          if (!nullable3.HasValue)
          {
            nullable1 = new Decimal?();
            nullable17 = nullable1;
          }
          else
            nullable17 = new Decimal?(-nullable3.GetValueOrDefault());
          nullable15 = nullable17;
        }
        else
        {
          Decimal? nullable18 = flag1 ? rctLine.UnbilledQty : rctLine.BaseUnbilledQty;
          nullable3 = nullable14;
          nullable1 = nullable18;
          Decimal? nullable19;
          if (!(nullable3.HasValue & nullable1.HasValue))
          {
            nullable2 = new Decimal?();
            nullable19 = nullable2;
          }
          else
            nullable19 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
          Decimal? nullable20 = nullable19;
          nullable1 = nullable9;
          Decimal num4 = 0M;
          Decimal? nullable21;
          if (!(nullable1.GetValueOrDefault() < num4 & nullable1.HasValue))
          {
            nullable1 = nullable9;
            nullable3 = nullable18;
            nullable21 = nullable1.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue ? nullable18 : nullable9;
          }
          else
          {
            nullable3 = nullable9;
            nullable2 = nullable20;
            Decimal? nullable22;
            if (!nullable2.HasValue)
            {
              nullable16 = new Decimal?();
              nullable22 = nullable16;
            }
            else
              nullable22 = new Decimal?(-nullable2.GetValueOrDefault());
            nullable1 = nullable22;
            if (!(nullable3.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue))
            {
              nullable21 = nullable9;
            }
            else
            {
              nullable1 = nullable20;
              if (!nullable1.HasValue)
              {
                nullable3 = new Decimal?();
                nullable21 = nullable3;
              }
              else
                nullable21 = new Decimal?(-nullable1.GetValueOrDefault());
            }
          }
          nullable15 = nullable21;
        }
        nullable1 = nullable15;
        Decimal num5 = 0M;
        if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
        {
          nullable1 = nullable15;
          nullable3 = nullable9;
          Decimal? nullable23;
          Decimal? nullable24;
          if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
          {
            // ISSUE: variable of a boxed type
            __Boxed<TGraph> graph = (object) ((PXGraphExtension<TGraph>) this).Base;
            nullable2 = nullable12;
            nullable16 = nullable15;
            Decimal? nullable25;
            if (!(nullable2.HasValue & nullable16.HasValue))
            {
              nullable23 = new Decimal?();
              nullable25 = nullable23;
            }
            else
              nullable25 = new Decimal?(nullable2.GetValueOrDefault() * nullable16.GetValueOrDefault());
            nullable3 = nullable25;
            nullable1 = nullable9;
            Decimal? nullable26;
            if (!(nullable3.HasValue & nullable1.HasValue))
            {
              nullable16 = new Decimal?();
              nullable26 = nullable16;
            }
            else
              nullable26 = new Decimal?(nullable3.GetValueOrDefault() / nullable1.GetValueOrDefault());
            nullable24 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) graph, nullable26));
          }
          else
            nullable24 = nullable12;
          Decimal? nullable27 = nullable24;
          int num6;
          if (flag2)
          {
            nullable1 = nullable15;
            nullable3 = nullable9;
            if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
            {
              num6 = poAccrualSplit == null ? 1 : 0;
              goto label_66;
            }
          }
          num6 = 0;
label_66:
          bool flag3 = num6 != 0;
          Decimal? nullable28;
          if (!POLineType.UsePOAccrual(rctLine.LineType) || POLineType.IsProjectDropShip(tran.LineType) && tran.DropshipExpenseRecording != "R")
            nullable28 = new Decimal?(0M);
          else if (poAccrualSplit != null)
          {
            nullable3 = poAccrualSplit.PPVAmt;
            Decimal? nullable29;
            if (!nullable3.HasValue)
            {
              nullable1 = new Decimal?();
              nullable29 = nullable1;
            }
            else
              nullable29 = new Decimal?(-nullable3.GetValueOrDefault());
            nullable28 = nullable29;
          }
          else if (flag3)
          {
            nullable23 = poAccrual.ReceivedCost;
            Decimal? billedCost = poAccrual.BilledCost;
            Decimal sign4 = tran.Sign;
            Decimal? nullable30 = nullable10;
            Decimal? nullable31 = nullable30.HasValue ? new Decimal?(sign4 * nullable30.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable32;
            if (!(billedCost.HasValue & nullable31.HasValue))
            {
              nullable30 = new Decimal?();
              nullable32 = nullable30;
            }
            else
              nullable32 = new Decimal?(billedCost.GetValueOrDefault() + nullable31.GetValueOrDefault());
            Decimal? nullable33 = nullable32;
            nullable16 = nullable23.HasValue & nullable33.HasValue ? new Decimal?(nullable23.GetValueOrDefault() - nullable33.GetValueOrDefault()) : new Decimal?();
            nullable2 = poAccrual.PPVAmt;
            nullable3 = nullable16.HasValue & nullable2.HasValue ? new Decimal?(nullable16.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            nullable1 = nullable13;
            Decimal? nullable34;
            if (!(nullable3.HasValue & nullable1.HasValue))
            {
              nullable2 = new Decimal?();
              nullable34 = nullable2;
            }
            else
              nullable34 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
            nullable28 = nullable34;
          }
          else
          {
            nullable1 = tran.POAccrualType == "R" ? rctLine.TranCostFinal : rctLine.TranCost;
            nullable3 = nullable14;
            Decimal? nullable35;
            if (!(nullable1.HasValue & nullable3.HasValue))
            {
              nullable2 = new Decimal?();
              nullable35 = nullable2;
            }
            else
              nullable35 = new Decimal?(nullable1.GetValueOrDefault() / nullable3.GetValueOrDefault());
            nullable3 = nullable15;
            nullable1 = nullable35;
            Decimal? nullable36;
            if (!(nullable3.HasValue & nullable1.HasValue))
            {
              nullable2 = new Decimal?();
              nullable36 = nullable2;
            }
            else
              nullable36 = new Decimal?(nullable3.GetValueOrDefault() * nullable1.GetValueOrDefault());
            Decimal? nullable37 = nullable36;
            nullable37 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) ((PXGraphExtension<TGraph>) this).Base, nullable37));
            nullable3 = nullable37;
            nullable2 = nullable27;
            Decimal? nullable38;
            if (!(nullable3.HasValue & nullable2.HasValue))
            {
              nullable16 = new Decimal?();
              nullable38 = nullable16;
            }
            else
              nullable38 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
            nullable1 = nullable38;
            Decimal apSign = this.GetAPSign(rctLine);
            Decimal? nullable39;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable39 = nullable2;
            }
            else
              nullable39 = new Decimal?(nullable1.GetValueOrDefault() * apSign);
            nullable28 = nullable39;
          }
          nullable1 = nullable13;
          nullable2 = nullable28;
          Decimal? nullable40;
          if (!(nullable1.HasValue & nullable2.HasValue))
          {
            nullable3 = new Decimal?();
            nullable40 = nullable3;
          }
          else
            nullable40 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
          nullable13 = nullable40;
          nullable2 = nullable9;
          nullable1 = nullable15;
          Decimal? nullable41;
          if (!(nullable2.HasValue & nullable1.HasValue))
          {
            nullable3 = new Decimal?();
            nullable41 = nullable3;
          }
          else
            nullable41 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
          nullable9 = nullable41;
          nullable1 = nullable12;
          nullable2 = nullable27;
          Decimal? nullable42;
          if (!(nullable1.HasValue & nullable2.HasValue))
          {
            nullable3 = new Decimal?();
            nullable42 = nullable3;
          }
          else
            nullable42 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
          nullable12 = nullable42;
          if (onAccrualSplitDefined != null)
            onAccrualSplitDefined(new OnAccrualSplitDefinedArgs()
            {
              ReceiptLineToBill = rctLine,
              SplitToReverse = poAccrualSplit,
              IsBaseQty = !flag1,
              ReceiptLineBillQty = nullable15,
              ReceiptLineBillAmount = nullable27,
              PPVAmount = nullable28
            });
        }
      }
      else
        break;
    }
    return new PPVCalculationResult(nullable13.GetValueOrDefault())
    {
      IsBaseQty = !flag1,
      Sign = sign1,
      BillQty = nullable9,
      AccrualBilledQty = nullable5
    };
  }

  protected virtual List<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>> CollectReceiptLinesForBilling(
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.AP.APTran tran,
    POAccrualStatus poAccrual)
  {
    List<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>> tupleList = new List<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>();
    if (tran.POAccrualType == null)
      return tupleList;
    if (tran.POAccrualType == "R")
    {
      PXResultset<PX.Objects.PO.POReceiptLine> pxResultset = PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoin<PX.Objects.PO.POReceiptLine, LeftJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[3]
      {
        (object) tran.ReceiptType,
        (object) tran.ReceiptNbr,
        (object) tran.ReceiptLineNbr
      });
      tupleList.Add(new Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>(PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResultset), (POAccrualSplit) null));
      return tupleList;
    }
    IEnumerable<PX.Objects.PO.POReceiptLine> source = GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceipt>.On<PX.Objects.PO.POReceiptLine.FK.Receipt>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.pOType, Equal<BqlField<PX.Objects.AP.APTran.pOOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.AP.APTran.pONbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.AP.APTran.pOLineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.pOAccrualType, IBqlString>.IsEqual<POAccrualType.order>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.PO.POReceipt.isUnderCorrection, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[1]
    {
      (object) tran
    }, Array.Empty<object>()));
    if (this.IsReverseAPTran(apdoc, tran))
    {
      IEnumerable<POAccrualSplit> origAccrualSplits = GraphHelper.RowCast<POAccrualSplit>((IEnumerable) PXSelectBase<POAccrualSplit, PXSelectReadonly<POAccrualSplit, Where<POAccrualSplit.refNoteID, Equal<Required<POAccrualSplit.refNoteID>>, And<POAccrualSplit.lineNbr, Equal<Required<POAccrualSplit.lineNbr>>, And<POAccrualSplit.type, Equal<Required<POAccrualSplit.type>>, And<POAccrualSplit.aPDocType, Equal<Required<POAccrualSplit.aPDocType>>, And<POAccrualSplit.aPRefNbr, Equal<Required<POAccrualSplit.aPRefNbr>>, And<POAccrualSplit.aPLineNbr, Equal<Required<POAccrualSplit.aPLineNbr>>, And<POAccrualSplit.isReversed, Equal<False>>>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[6]
      {
        (object) poAccrual.RefNoteID,
        (object) poAccrual.LineNbr,
        (object) poAccrual.Type,
        (object) apdoc.OrigDocType,
        (object) apdoc.OrigRefNbr,
        (object) tran.OrigLineNbr
      }));
      tupleList.AddRange(source.Select<PX.Objects.PO.POReceiptLine, Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>((Func<PX.Objects.PO.POReceiptLine, Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>) (r => new Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>(r, origAccrualSplits.FirstOrDefault<POAccrualSplit>((Func<POAccrualSplit, bool>) (s =>
      {
        if (!(s.POReceiptType == r.ReceiptType) || !(s.POReceiptNbr == r.ReceiptNbr))
          return false;
        int? poReceiptLineNbr = s.POReceiptLineNbr;
        int? lineNbr = r.LineNbr;
        return poReceiptLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poReceiptLineNbr.HasValue == lineNbr.HasValue;
      }))))).Where<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>((Func<Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>, bool>) (r => r.Item2 != null)));
    }
    else
      tupleList.AddRange(source.Select<PX.Objects.PO.POReceiptLine, Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>((Func<PX.Objects.PO.POReceiptLine, Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>>) (r => new Tuple<PX.Objects.PO.POReceiptLine, POAccrualSplit>(r, (POAccrualSplit) null))));
    return tupleList;
  }

  protected virtual ARReleaseProcess.Amount GetExpensePostingAmount(
    PXGraph graph,
    PX.Objects.AP.APTran tran,
    PX.Objects.AP.APInvoice invoice)
  {
    return APReleaseProcess.GetExpensePostingAmount(graph, tran);
  }

  protected virtual bool IsReverseAPTran(PX.Objects.AP.APInvoice apdoc, PX.Objects.AP.APTran tran)
  {
    return tran.Sign < 0M && !string.IsNullOrEmpty(apdoc.OrigDocType) && !string.IsNullOrEmpty(apdoc.OrigRefNbr) && tran.OrigLineNbr.HasValue;
  }

  protected Decimal GetAPSign(PX.Objects.PO.POReceiptLine rctLine)
  {
    short? invtMult = rctLine.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    return !(nullable.GetValueOrDefault() < num & nullable.HasValue) ? 1M : -1M;
  }

  protected PX.Objects.PO.POReceiptLine GetReceiptUnderCorrection(PX.Objects.AP.APTran tran)
  {
    if (string.IsNullOrEmpty(tran.POAccrualType) || !tran.POAccrualRefNoteID.HasValue || !tran.POAccrualLineNbr.HasValue)
      return (PX.Objects.PO.POReceiptLine) null;
    PXSelectBase<PX.Objects.PO.POReceiptLine> pxSelectBase = (PXSelectBase<PX.Objects.PO.POReceiptLine>) new PXSelectReadonly2<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>, Where<PX.Objects.PO.POReceiptLine.pOAccrualType, Equal<Current<PX.Objects.AP.APTran.pOAccrualType>>, And<PX.Objects.PO.POReceiptLine.pOAccrualRefNoteID, Equal<Current<PX.Objects.AP.APTran.pOAccrualRefNoteID>>, And<PX.Objects.PO.POReceiptLine.pOAccrualLineNbr, Equal<Current<PX.Objects.AP.APTran.pOAccrualLineNbr>>>>>>((PXGraph) ((PXGraphExtension<TGraph>) this).Base);
    if (tran.POAccrualType == "O")
      pxSelectBase.WhereAnd<Where<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<True>>>();
    else
      pxSelectBase.WhereAnd<Where2<Where<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<True>, Or<PX.Objects.PO.POReceipt.canceled, Equal<True>>>, And<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<PX.Objects.AP.APTran.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<PX.Objects.AP.APTran.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Current<PX.Objects.AP.APTran.receiptLineNbr>>>>>>>();
    return PXResult.Unwrap<PX.Objects.PO.POReceiptLine>(((PXSelectBase) pxSelectBase).View.SelectSingleBound((object[]) new PX.Objects.AP.APTran[1]
    {
      tran
    }, Array.Empty<object>()));
  }
}
