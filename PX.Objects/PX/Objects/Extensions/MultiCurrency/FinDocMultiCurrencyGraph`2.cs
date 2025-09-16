// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.FinDocMultiCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

/// <summary>The generic graph extension that defines the multi-currency functionality extended for AP/AR entities.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">A DAC (a <see cref="T:PX.Data.IBqlTable" /> type).</typeparam>
public abstract class FinDocMultiCurrencyGraph<TGraph, TPrimary> : 
  MultiCurrencyGraph<TGraph, TPrimary>,
  IPXCurrencyHelper
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>
  /// Override to specify the way current document status should be obtained. Override with returning Balanced status code if it is processing screen
  /// </summary>
  protected abstract string DocumentStatus { get; }

  /// <summary>
  /// Some base values should be recalculated even their Cury fields are marked as BaseCalc = false: DocBal and DiscBal, for example.
  /// Override this property to list them. This emulates behavior of old PXDBCurrencyAttribute.SetBaseCalc&lt;ARInvoice.curyDiscBal&gt;(cache, doc, state.BalanceBaseCalc);
  /// </summary>
  protected abstract IEnumerable<System.Type> FieldWhichShouldBeRecalculatedAnyway { get; }

  protected abstract bool ShouldBeDisabledDueToDocStatus();

  protected override bool AllowOverrideCury()
  {
    return base.AllowOverrideCury() && !this.ShouldBeDisabledDueToDocStatus();
  }

  protected override void DateFieldUpdated<CuryInfoID, DocumentDate>(PXCache sender, IBqlTable row)
  {
    if (this.ShouldBeDisabledDueToDocStatus())
      return;
    base.DateFieldUpdated<CuryInfoID, DocumentDate>(sender, row);
  }

  protected override bool ShouldMainCurrencyInfoBeReadonly()
  {
    if (this.Base.IsContractBasedAPI)
      return base.ShouldMainCurrencyInfoBeReadonly();
    return base.ShouldMainCurrencyInfoBeReadonly() || this.ShouldBeDisabledDueToDocStatus();
  }

  protected virtual void _(PX.Data.Events.RowSelected<TPrimary> e)
  {
    foreach (System.Type key in this.TrackedItems.Keys)
    {
      if (typeof (TPrimary).IsAssignableFrom(key))
      {
        List<CuryField> trackedItem = this.TrackedItems[key];
        foreach (System.Type type in this.FieldWhichShouldBeRecalculatedAnyway)
        {
          System.Type fieldToRecalculate = type;
          CuryField curyField = trackedItem.SingleOrDefault<CuryField>((Func<CuryField, bool>) (f => f.CuryName.Equals(fieldToRecalculate.Name, StringComparison.OrdinalIgnoreCase)));
          if (curyField != null)
            curyField.BaseCalc = !this.ShouldBeDisabledDueToDocStatus();
        }
      }
    }
  }

  internal void SetDetailCuryInfoID<T>(PXSelectBase<T> detailView, long? CuryInfoID) where T : class, IBqlTable, new()
  {
    foreach (PXResult<T> pxResult in detailView.Select())
    {
      T obj = (T) pxResult;
      long? nullable1 = detailView.Cache.GetValue<ARTran.curyInfoID>((object) obj) as long?;
      long? nullable2 = CuryInfoID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        detailView.Cache.SetValue<ARTran.curyInfoID>((object) obj, (object) CuryInfoID);
        this.recalculateRowBaseValues(detailView.Cache, (object) obj, (IEnumerable<CuryField>) this.TrackedItems[typeof (T)]);
        detailView.Cache.Update((object) obj);
      }
    }
  }

  private void RestoreDifferentCurrencyInfoIDs(IAdjustment aRAdjust)
  {
    long? adjdCuryInfoId = aRAdjust.AdjdCuryInfoID;
    long? adjgCuryInfoId1 = aRAdjust.AdjgCuryInfoID;
    if (adjdCuryInfoId.GetValueOrDefault() == adjgCuryInfoId1.GetValueOrDefault() & adjdCuryInfoId.HasValue == adjgCuryInfoId1.HasValue)
      aRAdjust.AdjdCuryInfoID = this.CloneCurrencyInfo(this.GetCurrencyInfo(aRAdjust.AdjgCuryInfoID)).CuryInfoID;
    long? adjdOrigCuryInfoId = aRAdjust.AdjdOrigCuryInfoID;
    long? adjgCuryInfoId2 = aRAdjust.AdjgCuryInfoID;
    if (!(adjdOrigCuryInfoId.GetValueOrDefault() == adjgCuryInfoId2.GetValueOrDefault() & adjdOrigCuryInfoId.HasValue == adjgCuryInfoId2.HasValue))
      return;
    aRAdjust.AdjdOrigCuryInfoID = this.CloneCurrencyInfo(this.GetCurrencyInfo(aRAdjust.AdjgCuryInfoID)).CuryInfoID;
  }

  /// <summary>
  /// On saving CurrencyInfo entities identical to some CurrencyInfo record with base currency, acumatica reuses same CurrencyInfoID instead,
  /// so Adjustment can have the same AdjgCuryInfoID, AdjdCuryInfoID, AdjdOrigCuryInfoID.
  /// This method clones CurrencyInfo entity to link different ones to this fields
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="adjustmentsView"></param>
  internal void RestoreDifferentCurrencyInfoIDs<T>(PXSelectBase<T> adjustmentsView) where T : class, IAdjustment, IBqlTable, new()
  {
    EnumerableExtensions.ForEach<T>(adjustmentsView.Cache.Cached.OfType<T>(), new System.Action<T>(this.RestoreDifferentCurrencyInfoIDs));
  }

  internal void SetAdjgCuryInfoID<T>(PXSelectBase<T> adjustmentsView, long? AdjgCuryInfoID) where T : class, IAdjustment, IBqlTable, new()
  {
    foreach (PXResult<T> pxResult in adjustmentsView.Select())
    {
      T row = (T) pxResult;
      long? adjgCuryInfoId = row.AdjgCuryInfoID;
      long? nullable = AdjgCuryInfoID;
      if (!(adjgCuryInfoId.GetValueOrDefault() == nullable.GetValueOrDefault() & adjgCuryInfoId.HasValue == nullable.HasValue))
      {
        row.AdjgCuryInfoID = AdjgCuryInfoID;
        this.recalculateRowBaseValues(adjustmentsView.Cache, (object) row, (IEnumerable<CuryField>) this.TrackedItems[typeof (T)]);
        adjustmentsView.Cache.Update((object) row);
      }
    }
  }
}
