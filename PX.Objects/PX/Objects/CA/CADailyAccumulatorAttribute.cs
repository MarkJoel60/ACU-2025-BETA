// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADailyAccumulatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CADailyAccumulatorAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowPersistedSubscriber
{
  private bool alreadyPersisted;

  private void update(CATran tran, CADailySummary summary, int sign)
  {
    if (tran.OrigTranType == "CBT")
      return;
    if (tran.Cleared.GetValueOrDefault())
    {
      if (tran.Released.GetValueOrDefault())
      {
        CADailySummary caDailySummary1 = summary;
        Decimal? releasedClearedCr = caDailySummary1.AmtReleasedClearedCr;
        Decimal? curyCreditAmt = tran.CuryCreditAmt;
        Decimal num1 = (Decimal) sign;
        Decimal? nullable1 = curyCreditAmt.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() * num1) : new Decimal?();
        caDailySummary1.AmtReleasedClearedCr = releasedClearedCr.HasValue & nullable1.HasValue ? new Decimal?(releasedClearedCr.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        CADailySummary caDailySummary2 = summary;
        Decimal? releasedClearedDr = caDailySummary2.AmtReleasedClearedDr;
        Decimal? nullable2 = tran.CuryDebitAmt;
        Decimal num2 = (Decimal) sign;
        Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num2) : new Decimal?();
        Decimal? nullable4;
        if (!(releasedClearedDr.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(releasedClearedDr.GetValueOrDefault() + nullable3.GetValueOrDefault());
        caDailySummary2.AmtReleasedClearedDr = nullable4;
      }
      else
      {
        CADailySummary caDailySummary3 = summary;
        Decimal? unreleasedClearedCr = caDailySummary3.AmtUnreleasedClearedCr;
        Decimal? curyCreditAmt = tran.CuryCreditAmt;
        Decimal num3 = (Decimal) sign;
        Decimal? nullable5 = curyCreditAmt.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() * num3) : new Decimal?();
        caDailySummary3.AmtUnreleasedClearedCr = unreleasedClearedCr.HasValue & nullable5.HasValue ? new Decimal?(unreleasedClearedCr.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        CADailySummary caDailySummary4 = summary;
        Decimal? unreleasedClearedDr = caDailySummary4.AmtUnreleasedClearedDr;
        Decimal? nullable6 = tran.CuryDebitAmt;
        Decimal num4 = (Decimal) sign;
        Decimal? nullable7 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * num4) : new Decimal?();
        Decimal? nullable8;
        if (!(unreleasedClearedDr.HasValue & nullable7.HasValue))
        {
          nullable6 = new Decimal?();
          nullable8 = nullable6;
        }
        else
          nullable8 = new Decimal?(unreleasedClearedDr.GetValueOrDefault() + nullable7.GetValueOrDefault());
        caDailySummary4.AmtUnreleasedClearedDr = nullable8;
      }
    }
    else if (tran.Released.GetValueOrDefault())
    {
      CADailySummary caDailySummary5 = summary;
      Decimal? releasedUnclearedCr = caDailySummary5.AmtReleasedUnclearedCr;
      Decimal? curyCreditAmt = tran.CuryCreditAmt;
      Decimal num5 = (Decimal) sign;
      Decimal? nullable9 = curyCreditAmt.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() * num5) : new Decimal?();
      caDailySummary5.AmtReleasedUnclearedCr = releasedUnclearedCr.HasValue & nullable9.HasValue ? new Decimal?(releasedUnclearedCr.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
      CADailySummary caDailySummary6 = summary;
      Decimal? releasedUnclearedDr = caDailySummary6.AmtReleasedUnclearedDr;
      Decimal? nullable10 = tran.CuryDebitAmt;
      Decimal num6 = (Decimal) sign;
      Decimal? nullable11 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * num6) : new Decimal?();
      Decimal? nullable12;
      if (!(releasedUnclearedDr.HasValue & nullable11.HasValue))
      {
        nullable10 = new Decimal?();
        nullable12 = nullable10;
      }
      else
        nullable12 = new Decimal?(releasedUnclearedDr.GetValueOrDefault() + nullable11.GetValueOrDefault());
      caDailySummary6.AmtReleasedUnclearedDr = nullable12;
    }
    else
    {
      CADailySummary caDailySummary7 = summary;
      Decimal? unreleasedUnclearedCr = caDailySummary7.AmtUnreleasedUnclearedCr;
      Decimal? curyCreditAmt = tran.CuryCreditAmt;
      Decimal num7 = (Decimal) sign;
      Decimal? nullable13 = curyCreditAmt.HasValue ? new Decimal?(curyCreditAmt.GetValueOrDefault() * num7) : new Decimal?();
      caDailySummary7.AmtUnreleasedUnclearedCr = unreleasedUnclearedCr.HasValue & nullable13.HasValue ? new Decimal?(unreleasedUnclearedCr.GetValueOrDefault() + nullable13.GetValueOrDefault()) : new Decimal?();
      CADailySummary caDailySummary8 = summary;
      Decimal? unreleasedUnclearedDr = caDailySummary8.AmtUnreleasedUnclearedDr;
      Decimal? nullable14 = tran.CuryDebitAmt;
      Decimal num8 = (Decimal) sign;
      Decimal? nullable15 = nullable14.HasValue ? new Decimal?(nullable14.GetValueOrDefault() * num8) : new Decimal?();
      Decimal? nullable16;
      if (!(unreleasedUnclearedDr.HasValue & nullable15.HasValue))
      {
        nullable14 = new Decimal?();
        nullable16 = nullable14;
      }
      else
        nullable16 = new Decimal?(unreleasedUnclearedDr.GetValueOrDefault() + nullable15.GetValueOrDefault());
      caDailySummary8.AmtUnreleasedUnclearedDr = nullable16;
    }
  }

  public static void RowInserted<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is CADailyAccumulatorAttribute)
        ((CADailyAccumulatorAttribute) attribute).RowInserted(sender, new PXRowInsertedEventArgs(data, false));
    }
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.alreadyPersisted = false;
    CATran row = (CATran) e.Row;
    if (!row.CashAccountID.HasValue || !row.TranDate.HasValue || !(row.OrigTranType != "CBT"))
      return;
    CADailySummary summary = (CADailySummary) sender.Graph.Caches[typeof (CADailySummary)].Insert((object) new CADailySummary()
    {
      CashAccountID = row.CashAccountID,
      TranDate = row.TranDate
    });
    this.update(row, summary, 1);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this.alreadyPersisted = false;
    CATran row = (CATran) e.Row;
    if (!row.CashAccountID.HasValue || !row.TranDate.HasValue || !(row.OrigTranType != "CBT"))
      return;
    CADailySummary summary = (CADailySummary) sender.Graph.Caches[typeof (CADailySummary)].Insert((object) new CADailySummary()
    {
      CashAccountID = row.CashAccountID,
      TranDate = row.TranDate
    });
    this.update(row, summary, -1);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.alreadyPersisted = false;
    CATran row = (CATran) e.Row;
    DateTime? tranDate;
    if (row.CashAccountID.HasValue)
    {
      tranDate = row.TranDate;
      if (tranDate.HasValue)
      {
        CADailySummary summary = this.InsertCADailySummary(sender, row);
        this.update(row, summary, 1);
      }
    }
    CATran oldRow = (CATran) e.OldRow;
    if (!oldRow.CashAccountID.HasValue)
      return;
    tranDate = oldRow.TranDate;
    if (!tranDate.HasValue)
      return;
    CADailySummary summary1 = this.InsertCADailySummary(sender, oldRow);
    this.update(oldRow, summary1, -1);
  }

  private CADailySummary InsertCADailySummary(PXCache cache, CATran tran)
  {
    CADailySummary caDailySummary = new CADailySummary();
    caDailySummary.CashAccountID = tran.CashAccountID;
    caDailySummary.TranDate = tran.TranDate;
    using (new PXReadBranchRestrictedScope())
      return (CADailySummary) cache.Graph.Caches[typeof (CADailySummary)].Insert((object) caDailySummary);
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null)
      return;
    this.alreadyPersisted = false;
  }

  private void CATranRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.alreadyPersisted)
      return;
    sender.Graph.Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
    this.alreadyPersisted = true;
  }

  private void CATranRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null)
      return;
    sender.Graph.Caches[typeof (CADailySummary)].Persisted(e.TranStatus == 2);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.RowPersisting.AddHandler<CATran>(new PXRowPersisting((object) this, __methodptr(CATranRowPersisting)));
    // ISSUE: method pointer
    sender.Graph.RowPersisted.AddHandler<CATran>(new PXRowPersisted((object) this, __methodptr(CATranRowPersisted)));
  }

  /// <summary>
  /// This method serves as a workaround for AC-192878, that fixes inconsistencies related to the alreadyPersisted flag
  /// </summary>
  /// <param name="sender"></param>
  internal static void PersistInserted(PXCache sender)
  {
    sender.Graph.Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
  }
}
