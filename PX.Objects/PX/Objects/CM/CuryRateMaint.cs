// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CuryRateMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CM;

[TableAndChartDashboardType]
public class CuryRateMaint : PXGraph<CuryRateMaint>
{
  public PXSave<CuryRateFilter> Save;
  public PXCancel<CuryRateFilter> Cancel;
  public PXAction<CuryRateFilter> first;
  public PXAction<CuryRateFilter> prev;
  public PXAction<CuryRateFilter> next;
  public PXAction<CuryRateFilter> last;
  public PXFilter<CuryRateFilter> Filter;
  [PXImport(typeof (CuryRateFilter))]
  public PXSelectOrderBy<CurrencyRate, OrderBy<Asc<CurrencyRate.fromCuryID, Asc<CurrencyRate.curyRateType>>>> CuryRateRecordsEntry;
  public PXSelectOrderBy<CurrencyRate2, OrderBy<Asc<CurrencyRate2.fromCuryID, Asc<CurrencyRate2.curyRateType>>>> CuryRateRecordsEffDate;
  public PXSetup<PX.Objects.CM.CMSetup> CMSetup;

  [PXFirstButton]
  [PXUIField]
  protected virtual IEnumerable First(PXAdapter a)
  {
    CuryRateMaint curyRateMaint = this;
    PXLongOperation.ClearStatus(((PXGraph) curyRateMaint).UID);
    CuryRateFilter curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current;
    if (curyRateFilter.ToCurrency != null)
    {
      CurrencyRate currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[1]
      {
        (object) curyRateFilter.ToCurrency
      });
      if (currencyRate != null)
      {
        CuryRateFilter copy = (CuryRateFilter) ((PXSelectBase) curyRateMaint.Filter).Cache.CreateCopy((object) ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current);
        copy.ToCurrency = currencyRate.ToCuryID;
        copy.EffDate = currencyRate.CuryEffDate;
        curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Update(copy);
        ((PXSelectBase) curyRateMaint.Filter).Cache.IsDirty = false;
      }
    }
    yield return (object) curyRateFilter;
  }

  [PXPreviousButton]
  [PXUIField]
  protected virtual IEnumerable Prev(PXAdapter a)
  {
    CuryRateMaint curyRateMaint = this;
    PXLongOperation.ClearStatus(((PXGraph) curyRateMaint).UID);
    CuryRateFilter curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current;
    if (curyRateFilter.ToCurrency != null)
    {
      CurrencyRate currencyRate;
      if (curyRateFilter.EffDate.HasValue)
        currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyEffDate, Less<Required<CurrencyRate.curyEffDate>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[2]
        {
          (object) curyRateFilter.ToCurrency,
          (object) curyRateFilter.EffDate
        });
      else
        currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[1]
        {
          (object) curyRateFilter.ToCurrency
        });
      if (currencyRate != null)
      {
        CuryRateFilter copy = (CuryRateFilter) ((PXSelectBase) curyRateMaint.Filter).Cache.CreateCopy((object) ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current);
        copy.ToCurrency = currencyRate.ToCuryID;
        copy.EffDate = currencyRate.CuryEffDate;
        curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Update(copy);
        ((PXSelectBase) curyRateMaint.Filter).Cache.IsDirty = false;
      }
    }
    yield return (object) curyRateFilter;
  }

  [PXNextButton]
  [PXUIField]
  protected virtual IEnumerable Next(PXAdapter a)
  {
    CuryRateMaint curyRateMaint = this;
    PXLongOperation.ClearStatus(((PXGraph) curyRateMaint).UID);
    CuryRateFilter curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current;
    if (curyRateFilter.ToCurrency != null)
    {
      CurrencyRate currencyRate;
      if (curyRateFilter.EffDate.HasValue)
        currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyEffDate, Greater<Required<CurrencyRate.curyEffDate>>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[2]
        {
          (object) curyRateFilter.ToCurrency,
          (object) curyRateFilter.EffDate
        });
      else
        currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[1]
        {
          (object) curyRateFilter.ToCurrency
        });
      if (currencyRate != null)
      {
        CuryRateFilter copy = (CuryRateFilter) ((PXSelectBase) curyRateMaint.Filter).Cache.CreateCopy((object) ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current);
        copy.ToCurrency = currencyRate.ToCuryID;
        copy.EffDate = currencyRate.CuryEffDate;
        curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Update(copy);
        ((PXSelectBase) curyRateMaint.Filter).Cache.IsDirty = false;
      }
    }
    yield return (object) curyRateFilter;
  }

  [PXLastButton]
  [PXUIField]
  protected virtual IEnumerable Last(PXAdapter a)
  {
    CuryRateMaint curyRateMaint = this;
    PXLongOperation.ClearStatus(((PXGraph) curyRateMaint).UID);
    CuryRateFilter curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current;
    if (curyRateFilter.ToCurrency != null)
    {
      CurrencyRate currencyRate = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>((PXGraph) curyRateMaint)).View.SelectSingle(new object[1]
      {
        (object) curyRateFilter.ToCurrency
      });
      if (currencyRate != null)
      {
        CuryRateFilter copy = (CuryRateFilter) ((PXSelectBase) curyRateMaint.Filter).Cache.CreateCopy((object) ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Current);
        copy.ToCurrency = currencyRate.ToCuryID;
        copy.EffDate = currencyRate.CuryEffDate;
        curyRateFilter = ((PXSelectBase<CuryRateFilter>) curyRateMaint.Filter).Update(copy);
        ((PXSelectBase) curyRateMaint.Filter).Cache.IsDirty = false;
      }
    }
    yield return (object) curyRateFilter;
  }

  protected virtual IEnumerable curyRateRecordsEntry()
  {
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    List<CurrencyRate> currencyRateList = new List<CurrencyRate>();
    foreach (PXResult<CurrencyRate> pxResult in PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyEffDate, Equal<Required<CurrencyRate.curyEffDate>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.ToCurrency,
      (object) current.EffDate
    }))
    {
      CurrencyRate currencyRate = PXResult<CurrencyRate>.op_Implicit(pxResult);
      currencyRateList.Add(currencyRate);
    }
    foreach (CurrencyRate currencyRate in ((PXSelectBase) this.CuryRateRecordsEntry).Cache.Inserted)
    {
      if (!currencyRateList.Contains(currencyRate))
        currencyRateList.Add(currencyRate);
    }
    foreach (CurrencyRate currencyRate in ((PXSelectBase) this.CuryRateRecordsEntry).Cache.Updated)
    {
      if (!currencyRateList.Contains(currencyRate))
        currencyRateList.Add(currencyRate);
    }
    return (IEnumerable) currencyRateList;
  }

  protected virtual IEnumerable curyRateRecordsEffDate()
  {
    PXSelectBase<CurrencyRate2> pxSelectBase = (PXSelectBase<CurrencyRate2>) new PXSelect<CurrencyRate2, Where<CurrencyRate2.toCuryID, Equal<Required<CurrencyRate2.toCuryID>>, And<CurrencyRate2.fromCuryID, Equal<Required<CurrencyRate2.fromCuryID>>, And<CurrencyRate2.curyRateType, Equal<Required<CurrencyRate2.curyRateType>>, And<CurrencyRate2.curyEffDate, Equal<Required<CurrencyRate2.curyEffDate>>>>>>>((PXGraph) this);
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    List<CurrencyRate2> currencyRate2List = new List<CurrencyRate2>();
    foreach (PXResult<CurrencyRate2> pxResult in PXSelectBase<CurrencyRate2, PXSelectGroupBy<CurrencyRate2, Where<CurrencyRate2.toCuryID, Equal<Current<CuryRateFilter.toCurrency>>, And<CurrencyRate2.curyEffDate, LessEqual<Current<CuryRateFilter.effDate>>>>, Aggregate<Max<CurrencyRate2.curyEffDate, GroupBy<CurrencyRate2.curyRateType, GroupBy<CurrencyRate2.fromCuryID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      CurrencyRate2 currencyRate2 = PXResult<CurrencyRate2>.op_Implicit(pxResult);
      currencyRate2List.Add(PXResultset<CurrencyRate2>.op_Implicit(pxSelectBase.Select(new object[4]
      {
        (object) current.ToCurrency,
        (object) currencyRate2.FromCuryID,
        (object) currencyRate2.CuryRateType,
        (object) currencyRate2.CuryEffDate
      })));
    }
    return (IEnumerable) currencyRate2List;
  }

  public CuryRateMaint()
  {
    PXCache cache1 = ((PXSelectBase) this.CuryRateRecordsEntry).Cache;
    PXCache cache2 = ((PXSelectBase) this.CuryRateRecordsEffDate).Cache;
    PXUIFieldAttribute.SetVisible<CurrencyRate.curyRateID>(cache1, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.rateReciprocal>(cache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<CurrencyRate2.curyRateID>(cache2, (object) null, false);
    PXUIFieldAttribute.SetEnabled<CurrencyRate.rateReciprocal>(cache2, (object) null, false);
    cache1.AllowInsert = true;
    cache1.AllowUpdate = true;
    cache1.AllowDelete = true;
    cache2.AllowDelete = false;
    cache2.AllowInsert = false;
    cache2.AllowUpdate = false;
  }

  protected virtual void CurrencyRate_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    CurrencyRate newRow = e.NewRow as CurrencyRate;
    if (!newRow.CuryEffDate.HasValue)
      newRow.CuryEffDate = new DateTime?(DateTime.Now);
    if (newRow.FromCuryID != null && newRow.ToCuryID != null && string.Compare(newRow.FromCuryID, newRow.ToCuryID, true) == 0)
      throw new PXException("The destination currency should be different from the source currency.");
    Decimal? curyRate = newRow.CuryRate;
    if (curyRate.HasValue)
    {
      curyRate = newRow.CuryRate;
      if (Math.Round(curyRate.Value, 8) == 0M)
        throw new PXException("Currency rate cannot be set to zero.");
      CurrencyRate currencyRate = newRow;
      Decimal num = (Decimal) 1;
      curyRate = newRow.CuryRate;
      Decimal? nullable = new Decimal?(Math.Round((curyRate.HasValue ? new Decimal?(num / curyRate.GetValueOrDefault()) : new Decimal?()).Value, 8));
      currencyRate.RateReciprocal = nullable;
      if (((CurrencyInfo) newRow).CheckRateVariance(cache))
        cache.RaiseExceptionHandling<CurrencyRate.curyRate>((object) newRow, (object) newRow.CuryRate, (Exception) new PXSetPropertyException("Rate variance exceeds the limit specified on the Currency Management Preferences form.", (PXErrorLevel) 2));
    }
    if (newRow.FromCuryID == null || newRow.CuryRateType == null || newRow.CuryRateType == null || !newRow.CuryEffDate.HasValue)
      return;
    curyRate = newRow.CuryRate;
    if (!curyRate.HasValue)
      return;
    CurrencyRate currencyRate1 = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, Equal<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(cache.Graph, new object[4]
    {
      (object) newRow.ToCuryID,
      (object) newRow.FromCuryID,
      (object) newRow.CuryRateType,
      (object) newRow.CuryEffDate
    }));
    if (currencyRate1 == null)
      return;
    if (((PXSelectBase<CurrencyRate>) this.CuryRateRecordsEntry).Locate(currencyRate1) == null)
    {
      CurrencyRate copy = (CurrencyRate) ((PXSelectBase) this.CuryRateRecordsEntry).Cache.CreateCopy((object) currencyRate1);
      copy.CuryRate = newRow.CuryRate;
      copy.CuryMultDiv = newRow.CuryMultDiv;
      copy.RateReciprocal = newRow.RateReciprocal;
      ((PXSelectBase<CurrencyRate>) this.CuryRateRecordsEntry).Delete(newRow);
      ((PXSelectBase<CurrencyRate>) this.CuryRateRecordsEntry).Update(copy);
    }
    cache.RaiseExceptionHandling<CurrencyRate.curyRate>((object) newRow, (object) newRow.CuryRate, (Exception) new PXSetPropertyException("The rate from {0} to {1} of the {2} rate type has already been entered for {3:d}.", (PXErrorLevel) 2, new object[4]
    {
      (object) newRow.FromCuryID,
      (object) newRow.ToCuryID,
      (object) newRow.CuryRateType,
      (object) newRow.CuryEffDate
    }));
  }

  protected virtual void CurrencyRate_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CurrencyRate row = e.Row as CurrencyRate;
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    if (row.FromCuryID != null && current.ToCurrency != null && string.Compare(row.FromCuryID, current.ToCurrency, true) == 0)
      throw new PXException("The destination currency should be different from the source currency.");
    bool flag = e.Row != null && cache.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
    DateTime? nullable1;
    Decimal? curyRate;
    if (e.ExternalCall && !flag)
    {
      nullable1 = row.CuryEffDate;
      if (!nullable1.HasValue)
      {
        nullable1 = current.EffDate;
        row.CuryEffDate = nullable1.HasValue ? current.EffDate : new DateTime?(DateTime.Now);
      }
      curyRate = row.CuryRate;
      if (curyRate.HasValue)
      {
        CurrencyRate currencyRate = row;
        Decimal num = (Decimal) 1;
        curyRate = row.CuryRate;
        Decimal? nullable2 = new Decimal?(Math.Round((curyRate.HasValue ? new Decimal?(num / curyRate.GetValueOrDefault()) : new Decimal?()).Value, 8));
        currencyRate.RateReciprocal = nullable2;
        if (((CurrencyInfo) row).CheckRateVariance(cache))
          cache.RaiseExceptionHandling<CurrencyRate.curyRate>((object) row, (object) row.CuryRate, (Exception) new PXSetPropertyException("Rate variance exceeds the limit specified on the Currency Management Preferences form.", (PXErrorLevel) 2));
      }
      if (row.CuryMultDiv == " " && row.FromCuryID != null && row.ToCuryID != null && row.CuryRateType != null)
      {
        nullable1 = row.CuryEffDate;
        if (nullable1.HasValue)
        {
          CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(cache.Graph, new object[4]
          {
            (object) row.FromCuryID,
            (object) row.ToCuryID,
            (object) row.CuryRateType,
            (object) row.CuryEffDate
          }));
          if (currencyRate != null)
            row.CuryMultDiv = currencyRate.CuryMultDiv;
        }
      }
    }
    if (row.FromCuryID == null || row.CuryRateType == null || row.CuryRateType == null)
      return;
    nullable1 = row.CuryEffDate;
    if (!nullable1.HasValue)
      return;
    curyRate = row.CuryRate;
    if (!curyRate.HasValue)
      return;
    CurrencyRate currencyRate1 = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, Equal<Required<CurrencyRate.curyEffDate>>>>>>>.Config>.Select(cache.Graph, new object[4]
    {
      (object) row.ToCuryID,
      (object) row.FromCuryID,
      (object) row.CuryRateType,
      (object) row.CuryEffDate
    }));
    if (currencyRate1 == null)
      return;
    ((CancelEventArgs) e).Cancel = true;
    CurrencyRate copy = (CurrencyRate) ((PXSelectBase) this.CuryRateRecordsEntry).Cache.CreateCopy((object) currencyRate1);
    copy.CuryRate = row.CuryRate;
    copy.CuryMultDiv = row.CuryMultDiv;
    copy.RateReciprocal = row.RateReciprocal;
    ((PXSelectBase<CurrencyRate>) this.CuryRateRecordsEntry).Update(copy);
    cache.RaiseExceptionHandling<CurrencyRate.curyRate>((object) row, (object) row.CuryRate, (Exception) new PXSetPropertyException("The rate from {0} to {1} of the {2} rate type has already been entered for {3:d}.", (PXErrorLevel) 2, new object[4]
    {
      (object) row.FromCuryID,
      (object) row.ToCuryID,
      (object) row.CuryRateType,
      (object) row.CuryEffDate
    }));
  }

  protected virtual void CurrencyRate_FromCuryID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    if (e.NewValue == null || current.ToCurrency == null || string.Compare((string) e.NewValue, current.ToCurrency, true) != 0)
      return;
    cache.RaiseExceptionHandling("FromCuryID", e.Row, e.NewValue, (Exception) new PXSetPropertyException("The destination currency should be different from the source currency."));
    cache.SetStatus(e.Row, (PXEntryStatus) 0);
  }

  protected virtual void CurrencyRate_FromCuryID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    CurrencyRate row = (CurrencyRate) e.Row;
    if (row == null || row.FromCuryID == null || row.CuryRateType == null || !row.CuryEffDate.HasValue)
      return;
    CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(cache.Graph, new object[4]
    {
      (object) row.ToCuryID,
      (object) row.FromCuryID,
      (object) row.CuryRateType,
      (object) row.CuryEffDate
    }));
    if (currencyRate == null)
      return;
    row.CuryMultDiv = currencyRate.CuryMultDiv;
  }

  protected virtual void CurrencyRate_CuryRateType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    CurrencyRate row = (CurrencyRate) e.Row;
    if (row == null || row.FromCuryID == null || row.CuryRateType == null || !row.CuryEffDate.HasValue)
      return;
    CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(cache.Graph, new object[4]
    {
      (object) row.ToCuryID,
      (object) row.FromCuryID,
      (object) row.CuryRateType,
      (object) row.CuryEffDate
    }));
    if (currencyRate == null)
      return;
    row.CuryMultDiv = currencyRate.CuryMultDiv;
  }

  protected virtual void CurrencyRate_CuryEffDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CuryRateFilter current = ((PXSelectBase<CuryRateFilter>) this.Filter).Current;
    CurrencyRate row = (CurrencyRate) e.Row;
    if (row == null || row.FromCuryID == null || row.CuryRateType == null || !row.CuryEffDate.HasValue)
      return;
    CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(cache.Graph, new object[4]
    {
      (object) row.ToCuryID,
      (object) row.FromCuryID,
      (object) row.CuryRateType,
      (object) row.CuryEffDate
    }));
    if (currencyRate == null)
      return;
    row.CuryMultDiv = currencyRate.CuryMultDiv;
  }

  protected virtual void CurrencyRate_CuryRateID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 124) != 32 /*0x20*/)
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.IsRestriction = true;
    e.Expr = (SQLExpression) new Column<CurrencyRate.curyRateID>((Table) null);
    e.DataValue = (object) PXDatabase.SelectIdentity();
  }

  protected virtual void CurrencyRate_CuryRateType_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 124) != 32 /*0x20*/)
      return;
    e.IsRestriction = true;
  }

  protected virtual void CurrencyRate_FromCuryID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 124) != 32 /*0x20*/)
      return;
    e.IsRestriction = true;
  }

  protected virtual void CurrencyRate_ToCuryID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 124) != 32 /*0x20*/)
      return;
    e.IsRestriction = true;
  }

  protected virtual void CurrencyRate_CuryEffDate_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 124) != 32 /*0x20*/)
      return;
    e.IsRestriction = true;
  }

  protected virtual void CuryRateFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CuryRateFilter row = (CuryRateFilter) e.Row;
    if (row == null)
      return;
    bool flag1 = false;
    bool flag2 = false;
    if (row.ToCurrency != null && row.EffDate.HasValue)
    {
      CurrencyRate currencyRate1 = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyEffDate, Greater<Required<CurrencyRate.curyEffDate>>>>, OrderBy<Asc<CurrencyRate.curyEffDate>>>((PXGraph) this)).View.SelectSingle(new object[2]
      {
        (object) row.ToCurrency,
        (object) row.EffDate
      });
      CurrencyRate currencyRate2 = (CurrencyRate) ((PXSelectBase) new PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyEffDate, Less<Required<CurrencyRate.curyEffDate>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>((PXGraph) this)).View.SelectSingle(new object[2]
      {
        (object) row.ToCurrency,
        (object) row.EffDate
      });
      flag1 = currencyRate1 != null;
      flag2 = currencyRate2 > null;
      if (!((PXGraph) this).IsImport)
      {
        bool isDirty = ((PXSelectBase) this.CuryRateRecordsEntry).Cache.IsDirty;
        PXUIFieldAttribute.SetEnabled<CuryRateFilter.toCurrency>(cache, (object) row, !isDirty);
        PXUIFieldAttribute.SetEnabled<CuryRateFilter.effDate>(cache, (object) row, !isDirty);
      }
    }
    bool flag3 = flag2;
    bool flag4 = flag1;
    ((PXAction) this.next).SetEnabled(flag1);
    ((PXAction) this.prev).SetEnabled(flag2);
    ((PXAction) this.first).SetEnabled(flag3);
    ((PXAction) this.last).SetEnabled(flag4);
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Select(Array.Empty<object>()).Count == 0)
      throw new PXException("The required configuration data is not entered on the Currency Management Preferences (CM101000) form.");
    ((PXGraph) this).Persist();
  }
}
