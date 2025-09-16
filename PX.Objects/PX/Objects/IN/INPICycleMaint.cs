// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPICycleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INPICycleMaint : PXGraph<INPICycleMaint>
{
  public PXSelect<INPICycle> PICycles;
  public PXSavePerRow<INPICycle> Save;
  public PXCancel<INPICycle> Cancel;

  public virtual void INPICycle_CountsPerYear_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || (short) e.NewValue >= (short) 0 && (short) e.NewValue <= (short) 365)
      return;
    cache.RaiseExceptionHandling<INPICycle.countsPerYear>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("This value should be between {0} and {1}", (PXErrorLevel) 4, new object[2]
    {
      (object) 0,
      (object) 365
    }));
  }

  public virtual void INPICycle_MaxCountInaccuracyPct_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue < 0M || (Decimal) e.NewValue > 100M)
      throw new PXSetPropertyException("Percentage value should be between 0 and 100");
  }

  public virtual void INPICycle_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is INPICycle row) || (e.Operation & 3) == 3)
      return;
    short? countsPerYear = row.CountsPerYear;
    if (countsPerYear.HasValue)
    {
      countsPerYear = row.CountsPerYear;
      if (countsPerYear.Value < (short) 0)
        goto label_5;
    }
    countsPerYear = row.CountsPerYear;
    if (countsPerYear.Value <= (short) 365)
      return;
label_5:
    cache.RaiseExceptionHandling<INPICycle.countsPerYear>(e.Row, (object) row.CountsPerYear, (Exception) new PXSetPropertyException("This value should be between {0} and {1}", (PXErrorLevel) 4, new object[2]
    {
      (object) 0,
      (object) 365
    }));
  }
}
