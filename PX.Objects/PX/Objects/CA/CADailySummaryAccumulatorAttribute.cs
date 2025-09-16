// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADailySummaryAccumulatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class CADailySummaryAccumulatorAttribute : PXAccumulatorAttribute
{
  protected PXAccumulatorCollection _columns;
  protected Dictionary<object, List<object>> _chunks;
  protected HashSet<object> _persisted;

  public CADailySummaryAccumulatorAttribute() => this.SingleRecord = true;

  public virtual bool PersistInserted(PXCache sender, object row)
  {
    if (!base.PersistInserted(sender, row))
      return false;
    this._persisted.Add(row);
    return true;
  }

  public virtual object Insert(PXCache sender, object row)
  {
    object key1 = sender.Locate(row);
    if (key1 == null)
      return base.Insert(sender, row);
    bool flag;
    if (!(flag = this._persisted.Contains(key1)) && sender.GetStatus(key1) == 2)
    {
      sender.Current = key1;
      return key1;
    }
    sender.Remove(key1);
    if (!flag)
      return base.Insert(sender, row);
    object key2 = base.Insert(sender, row);
    sender.ResetPersisted(key1);
    List<object> objectList;
    if (!this._chunks.TryGetValue(key1, out objectList))
      objectList = new List<object>();
    this._chunks[key2] = objectList;
    objectList.Add(key1);
    this._chunks.Remove(key1);
    this._persisted.Remove(key1);
    return key2;
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    this._columns = columns;
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    CADailySummary cADailySummary = row as CADailySummary;
    if (sender.GetStatus(row) == 2 && this.IsZero(cADailySummary) && sender.Locate(row) is CADailySummary caDailySummary && caDailySummary == row)
    {
      sender.SetStatus(row, (PXEntryStatus) 4);
      return false;
    }
    return !this.SkipPersist(sender);
  }

  protected virtual bool SkipPersist(PXCache sender)
  {
    if (sender.Graph is JournalEntry graph1)
    {
      JournalEntry.JournalEntryContextExt extension = ((PXGraph) graph1).GetExtension<JournalEntry.JournalEntryContextExt>();
      if (extension != null)
        return extension.GraphContext == GraphContextExtention<JournalEntry>.Context.Release;
    }
    if (sender.Graph is ARPaymentEntry graph2)
    {
      ARPaymentEntry.ARPaymentContextExtention extension = ((PXGraph) graph2).GetExtension<ARPaymentEntry.ARPaymentContextExtention>();
      if (extension != null)
        return extension.GraphContext == GraphContextExtention<ARPaymentEntry>.Context.Persist;
    }
    return false;
  }

  protected virtual bool IsZero(CADailySummary cADailySummary)
  {
    Decimal? releasedClearedDr = cADailySummary.AmtReleasedClearedDr;
    Decimal num1 = 0M;
    if (releasedClearedDr.GetValueOrDefault() == num1 & releasedClearedDr.HasValue)
    {
      Decimal? unreleasedClearedDr = cADailySummary.AmtUnreleasedClearedDr;
      Decimal num2 = 0M;
      if (unreleasedClearedDr.GetValueOrDefault() == num2 & unreleasedClearedDr.HasValue)
      {
        Decimal? releasedUnclearedDr = cADailySummary.AmtReleasedUnclearedDr;
        Decimal num3 = 0M;
        if (releasedUnclearedDr.GetValueOrDefault() == num3 & releasedUnclearedDr.HasValue)
        {
          Decimal? unreleasedUnclearedDr = cADailySummary.AmtUnreleasedUnclearedDr;
          Decimal num4 = 0M;
          if (unreleasedUnclearedDr.GetValueOrDefault() == num4 & unreleasedUnclearedDr.HasValue)
          {
            Decimal? releasedClearedCr = cADailySummary.AmtReleasedClearedCr;
            Decimal num5 = 0M;
            if (releasedClearedCr.GetValueOrDefault() == num5 & releasedClearedCr.HasValue)
            {
              Decimal? unreleasedClearedCr = cADailySummary.AmtUnreleasedClearedCr;
              Decimal num6 = 0M;
              if (unreleasedClearedCr.GetValueOrDefault() == num6 & unreleasedClearedCr.HasValue)
              {
                Decimal? releasedUnclearedCr = cADailySummary.AmtReleasedUnclearedCr;
                Decimal num7 = 0M;
                if (releasedUnclearedCr.GetValueOrDefault() == num7 & releasedUnclearedCr.HasValue)
                {
                  Decimal? unreleasedUnclearedCr = cADailySummary.AmtUnreleasedUnclearedCr;
                  Decimal num8 = 0M;
                  if (unreleasedUnclearedCr.GetValueOrDefault() == num8 & unreleasedUnclearedCr.HasValue)
                    return true;
                }
              }
            }
          }
        }
      }
    }
    return false;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType1 = sender.GetItemType();
    CADailySummaryAccumulatorAttribute accumulatorAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) accumulatorAttribute1, __vmethodptr(accumulatorAttribute1, RowPersisting));
    rowPersisting.AddHandler(itemType1, pxRowPersisting);
    PXGraph.RowPersistedEvents rowPersisted = sender.Graph.RowPersisted;
    Type itemType2 = sender.GetItemType();
    CADailySummaryAccumulatorAttribute accumulatorAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) accumulatorAttribute2, __vmethodptr(accumulatorAttribute2, RowPersisted));
    rowPersisted.AddHandler(itemType2, pxRowPersisted);
    this._chunks = new Dictionary<object, List<object>>();
    this._persisted = new HashSet<object>();
  }

  protected virtual object Aggregate(PXCache cache, object a, object b)
  {
    object copy = cache.CreateCopy(a);
    foreach (KeyValuePair<string, PXAccumulatorItem> column in (Dictionary<string, PXAccumulatorItem>) this._columns)
    {
      if (column.Value.CurrentUpdateBehavior == 1)
      {
        object obj1 = cache.GetValue(a, column.Key);
        object obj2 = cache.GetValue(b, column.Key);
        object obj3 = (object) null;
        if (obj1.GetType() == typeof (Decimal))
          obj3 = (object) ((Decimal) obj1 + (Decimal) obj2);
        if (obj1.GetType() == typeof (double))
          obj3 = (object) ((double) obj1 + (double) obj2);
        if (obj1.GetType() == typeof (long))
          obj3 = (object) ((long) obj1 + (long) obj2);
        if (obj1.GetType() == typeof (int))
          obj3 = (object) ((int) obj1 + (int) obj2);
        if (obj1.GetType() == typeof (short))
          obj3 = (object) ((int) (short) obj1 + (int) (short) obj2);
        cache.SetValue(copy, column.Key, obj3);
      }
    }
    return copy;
  }

  protected virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CADailySummary row = e.Row as CADailySummary;
    if (row.TranDate.Value.TimeOfDay != TimeSpan.Zero)
    {
      string str1 = "TranDate";
      string str2 = "CADailySummary";
      throw new PXRowPersistingException(str1, (object) row.TranDate, "The {0} value of the {1} table is incorrect because it contains a time component. The format must be yyyy-mm-dd.", new object[2]
      {
        (object) str1,
        (object) str2
      });
    }
  }

  protected virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == 2)
    {
      foreach (KeyValuePair<object, List<object>> chunk in this._chunks)
      {
        foreach (object b in chunk.Value)
        {
          object obj = this.Aggregate(sender, chunk.Key, b);
          sender.RestoreCopy(chunk.Key, obj);
        }
      }
    }
    if (e.TranStatus == null)
      return;
    this._chunks = new Dictionary<object, List<object>>();
    this._persisted = new HashSet<object>();
  }
}
