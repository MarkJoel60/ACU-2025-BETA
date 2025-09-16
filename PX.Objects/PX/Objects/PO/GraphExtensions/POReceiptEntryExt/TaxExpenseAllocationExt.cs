// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.TaxExpenseAllocationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO.Services.AmountDistribution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class TaxExpenseAllocationExt : 
  PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() || PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>();
  }

  [InjectDependency]
  public AmountDistributionFactory AmountDistributionFactory { get; set; }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.UpdatePOOnRelease.InsertPOAccrualSplits(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POAccrualStatus,System.String,System.Nullable{System.Decimal},System.Nullable{System.Decimal},System.Nullable{System.Decimal})" />
  /// </summary>
  [PXOverride]
  public virtual void InsertPOAccrualSplits(
    POReceiptLine rctLine,
    POAccrualStatus poAccrual,
    string accruedUom,
    Decimal? accruedQty,
    Decimal? baseAccruedQty,
    Decimal? accruedCost,
    Action<POReceiptLine, POAccrualStatus, string, Decimal?, Decimal?, Decimal?> baseMethod)
  {
    List<POAccrualSplit> pOAccrualSplits = this.CollectPOAccrualSplits(rctLine, poAccrual);
    baseMethod(rctLine, poAccrual, accruedUom, accruedQty, baseAccruedQty, accruedCost);
    this.ApplyTaxAmount(rctLine, poAccrual, accruedQty, baseAccruedQty, pOAccrualSplits);
  }

  protected virtual List<POAccrualSplit> CollectPOAccrualSplits(
    POReceiptLine rctLine,
    POAccrualStatus poAccrual)
  {
    return GraphHelper.RowCast<APTranReceiptUpdate>((IEnumerable) ((PXSelectBase) this.Base2.apTranUpdate).View.SelectMultiBound(new object[1]
    {
      (object) poAccrual
    }, Array.Empty<object>()).AsEnumerable<object>()).Select<APTranReceiptUpdate, POAccrualSplit>((Func<APTranReceiptUpdate, POAccrualSplit>) (tran => new POAccrualSplit()
    {
      RefNoteID = poAccrual.RefNoteID,
      LineNbr = poAccrual.LineNbr,
      Type = poAccrual.Type,
      APDocType = tran.TranType,
      APRefNbr = tran.RefNbr,
      APLineNbr = tran.LineNbr,
      POReceiptType = rctLine.ReceiptType,
      POReceiptNbr = rctLine.ReceiptNbr,
      POReceiptLineNbr = rctLine.LineNbr
    })).ToList<POAccrualSplit>();
  }

  protected virtual void ApplyTaxAmount(
    POReceiptLine rctLine,
    POAccrualStatus poAccrual,
    Decimal? accruedQty,
    Decimal? baseAccruedQty,
    List<POAccrualSplit> pOAccrualSplits)
  {
    Decimal amountToDistribute = this.CalculateTaxAmountToDistribute(poAccrual, accruedQty, baseAccruedQty);
    poAccrual = this.ApplyTaxAmountToPOAccrual(rctLine, poAccrual, amountToDistribute);
    this.DistributeTaxAmountToSplits(rctLine, pOAccrualSplits, amountToDistribute);
  }

  protected virtual Decimal CalculateTaxAmountToDistribute(
    POAccrualStatus poAccrual,
    Decimal? accruedQty,
    Decimal? baseAccruedQty)
  {
    Decimal valueOrDefault1;
    Decimal? nullable;
    Decimal valueOrDefault2;
    if (accruedQty.HasValue)
    {
      valueOrDefault1 = accruedQty.GetValueOrDefault();
      Decimal? billedQty = poAccrual.BilledQty;
      nullable = poAccrual.ReceivedQty;
      valueOrDefault2 = (billedQty.HasValue & nullable.HasValue ? new Decimal?(billedQty.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    }
    else
    {
      valueOrDefault1 = baseAccruedQty.GetValueOrDefault();
      Decimal? baseBilledQty = poAccrual.BaseBilledQty;
      nullable = poAccrual.BaseReceivedQty;
      valueOrDefault2 = (baseBilledQty.HasValue & nullable.HasValue ? new Decimal?(baseBilledQty.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    }
    Decimal? billedTaxAdjCost = poAccrual.BilledTaxAdjCost;
    nullable = poAccrual.ReceivedTaxAdjCost;
    return (billedTaxAdjCost.HasValue & nullable.HasValue ? new Decimal?(billedTaxAdjCost.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault() * valueOrDefault1 / valueOrDefault2;
  }

  protected virtual POAccrualStatus ApplyTaxAmountToPOAccrual(
    POReceiptLine rctLine,
    POAccrualStatus poAccrual,
    Decimal taxAmountToDistribute)
  {
    POAccrualStatus poAccrualStatus1 = poAccrual;
    Decimal? nullable = poAccrualStatus1.ReceivedTaxAdjCost;
    Decimal num1 = taxAmountToDistribute;
    poAccrualStatus1.ReceivedTaxAdjCost = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num1) : new Decimal?();
    POAccrualStatus poAccrualStatus2 = poAccrual;
    nullable = poAccrualStatus2.ReceivedCost;
    Decimal num2 = taxAmountToDistribute;
    poAccrualStatus2.ReceivedCost = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num2) : new Decimal?();
    poAccrual = ((PXSelectBase<POAccrualStatus>) this.Base2.poAccrualUpdate).Update(poAccrual);
    POAccrualDetail poAccrualDetail1 = this.Base2.PreparePOReceiptLineAccrualDetail(rctLine);
    if (poAccrualDetail1 != null)
    {
      POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
      nullable = poAccrualDetail2.TaxAccruedCost;
      Decimal num3 = taxAmountToDistribute;
      poAccrualDetail2.TaxAccruedCost = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
      POAccrualDetail poAccrualDetail3 = poAccrualDetail1;
      nullable = poAccrualDetail3.AccruedCost;
      Decimal num4 = taxAmountToDistribute;
      poAccrualDetail3.AccruedCost = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num4) : new Decimal?();
      ((PXSelectBase<POAccrualDetail>) this.Base2.poAccrualDetailUpdate).Update(poAccrualDetail1);
    }
    POReceiptLine poReceiptLine = rctLine;
    nullable = poReceiptLine.TranCostFinal;
    Decimal num5 = taxAmountToDistribute;
    poReceiptLine.TranCostFinal = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
    return poAccrual;
  }

  protected virtual void DistributeTaxAmountToSplits(
    POReceiptLine rctLine,
    List<POAccrualSplit> pOAccrualSplits,
    Decimal taxAmountToDistribute)
  {
    IEnumerable<TaxExpenseAllocationExt.Split> splits = pOAccrualSplits.Select<POAccrualSplit, TaxExpenseAllocationExt.Split>((Func<POAccrualSplit, TaxExpenseAllocationExt.Split>) (s => new TaxExpenseAllocationExt.Split()
    {
      POAccrualSplit = (POAccrualSplit) ((PXSelectBase) this.Base2.poAccrualSplitUpdate).Cache.Locate((object) s)
    })).Where<TaxExpenseAllocationExt.Split>((Func<TaxExpenseAllocationExt.Split, bool>) (s => s.POAccrualSplit != null));
    this.AmountDistributionFactory.CreateService<TaxExpenseAllocationExt.Split>(DistributionMethod.RemainderToBiggestLine, new DistributionParameter<TaxExpenseAllocationExt.Split>()
    {
      Items = splits,
      Amount = new Decimal?(taxAmountToDistribute),
      CuryAmount = new Decimal?(taxAmountToDistribute),
      CuryRow = (object) rctLine,
      CacheOfCuryRow = ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache,
      OnValueCalculated = (Func<TaxExpenseAllocationExt.Split, Decimal?, Decimal?, TaxExpenseAllocationExt.Split>) ((item, amount, curyAmount) =>
      {
        item.POAccrualSplit = ((PXSelectBase<POAccrualSplit>) this.Base2.poAccrualSplitUpdate).Update(item.POAccrualSplit);
        return item;
      }),
      OnRoundingDifferenceApplied = (Action<TaxExpenseAllocationExt.Split, Decimal?, Decimal?, Decimal?, Decimal?>) ((item, newAmount, curyNewAmount, oldAmount, curyOldAmount) => item.POAccrualSplit = ((PXSelectBase<POAccrualSplit>) this.Base2.poAccrualSplitUpdate).Update(item.POAccrualSplit))
    }).Distribute();
  }

  protected class Split : IAmountItem
  {
    public POAccrualSplit POAccrualSplit { get; set; }

    public Decimal Weight
    {
      get
      {
        return (this.POAccrualSplit.AccruedQty ?? this.POAccrualSplit.BaseAccruedQty).GetValueOrDefault();
      }
    }

    public Decimal? Amount
    {
      get => this.POAccrualSplit.TaxAccruedCost;
      set => this.POAccrualSplit.TaxAccruedCost = value;
    }

    public Decimal? CuryAmount { get; set; }
  }
}
