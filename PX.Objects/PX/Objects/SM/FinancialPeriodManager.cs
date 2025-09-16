// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.FinancialPeriodManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.RelativeDates;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SM;

public class FinancialPeriodManager : IFinancialPeriodManager
{
  private readonly ITodayUtc todayer;

  private FinancialPeriodManager.FinancialPeriodDefinition Definition
  {
    get
    {
      return PXDatabase.GetSlot<FinancialPeriodManager.FinancialPeriodDefinition>("FinancialPeriodDefinition", FinancialPeriodManager.FinancialPeriodDefinition.DependentTables);
    }
  }

  private int CurrentIndex
  {
    get
    {
      int periodIndex = this.Definition.FindPeriodIndex(this.todayer.TodayUtc);
      return periodIndex != -1 ? periodIndex : throw new PXException("Cannot determine current financial period");
    }
  }

  public FinancialPeriodManager(ITodayUtc today) => this.todayer = today;

  public DateTime GetCurrentFinancialPeriodStart(int shift)
  {
    return this.GetFinancialPeriod(shift).StartDate.Value;
  }

  public DateTime GetCurrentFinancialPeriodEnd(int shift)
  {
    return this.GetFinancialPeriod(shift).EndDate.Value.AddTicks(-1L);
  }

  private FinPeriod GetFinancialPeriod(int shift)
  {
    int index = this.CurrentIndex + shift;
    if (index < 0 || index > this.Definition.Periods.Count - 1)
      throw new PXException("Cannot determine resulting financial period: shift value is too big");
    return this.Definition.Periods[index];
  }

  private class FinancialPeriodDefinition : IPrefetchable, IPXCompanyDependent
  {
    public const string SLOT_KEY = "FinancialPeriodDefinition";
    private readonly ConcurrentDictionary<DateTime, int> _periodsIndexes;

    public static Type[] DependentTables
    {
      get => new Type[1]{ typeof (FinPeriod) };
    }

    public List<FinPeriod> Periods { get; private set; }

    public FinancialPeriodDefinition()
    {
      this.Periods = new List<FinPeriod>();
      this._periodsIndexes = new ConcurrentDictionary<DateTime, int>();
    }

    public void Prefetch()
    {
      this.Periods.Clear();
      this._periodsIndexes.Clear();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<FinPeriod>(new PXDataField[4]
      {
        (PXDataField) new PXDataFieldOrder<FinPeriod.finPeriodID>(),
        (PXDataField) new PXDataField<FinPeriod.startDate>(),
        (PXDataField) new PXDataField<FinPeriod.endDate>(),
        (PXDataField) new PXDataFieldValue<FinPeriod.organizationID>((PXDbType) 8, new int?(4), (object) 0)
      }))
        this.Periods.Add(new FinPeriod()
        {
          StartDate = pxDataRecord.GetDateTime(0),
          EndDate = pxDataRecord.GetDateTime(1)
        });
    }

    public int FindPeriodIndex(DateTime dateTime)
    {
      return this._periodsIndexes.GetOrAdd(dateTime.Date, (Func<DateTime, int>) (date => this.Periods.FindIndex((Predicate<FinPeriod>) (p =>
      {
        DateTime? startDate = p.StartDate;
        DateTime dateTime1 = date;
        if ((startDate.HasValue ? (startDate.GetValueOrDefault() <= dateTime1 ? 1 : 0) : 0) == 0)
          return false;
        DateTime? endDate = p.EndDate;
        DateTime dateTime2 = date;
        return endDate.HasValue && endDate.GetValueOrDefault() > dateTime2;
      }))));
    }
  }
}
