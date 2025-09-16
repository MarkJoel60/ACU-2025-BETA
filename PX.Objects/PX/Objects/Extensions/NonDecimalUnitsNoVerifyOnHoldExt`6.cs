// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.NonDecimalUnitsNoVerifyOnHoldExt`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions;

public abstract class NonDecimalUnitsNoVerifyOnHoldExt<TGraph, TDocument, TLine, TLineQty, TLineSplit, TLineSplitQty> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
  where TLineQty : class, IBqlField
  where TLineSplit : class, IBqlTable, new()
  where TLineSplitQty : class, IBqlField
{
  public abstract bool HaveHoldStatus(TDocument doc);

  public abstract int? GetLineNbr(TLine line);

  public abstract int? GetLineNbr(TLineSplit split);

  protected abstract TLine LocateLine(TLineSplit split);

  public abstract IEnumerable<TLine> GetLines();

  public abstract IEnumerable<TLineSplit> GetSplits();

  protected virtual void _(PX.Data.Events.RowSelected<TDocument> e)
  {
    if ((object) e.Row == null)
      return;
    this.UpdateDecimalVerifyMode(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<TDocument> e)
  {
    if (this.HaveHoldStatus(e.OldRow) == this.HaveHoldStatus(e.Row))
      return;
    this.UpdateDecimalVerifyMode(e.Row);
    PXCache<TLine> pxCache = this.Base.Caches<TLine>();
    PXCache<TLineSplit> lineSplitCache = this.Base.Caches<TLineSplit>();
    ILookup<int?, TLineSplit> lookup = this.GetSplits().ToLookup<TLineSplit, int?>(new Func<TLineSplit, int?>(this.GetLineNbr));
    foreach (TLine line in this.GetLines())
    {
      this.VerifyLine((PXCache) pxCache, line);
      foreach (TLineSplit split in lookup[this.GetLineNbr(line)])
      {
        using (new NotDecimalUnitErrorRedirectorScope<TLineSplitQty, TLineQty>((PXCache) pxCache, (object) line))
          this.VerifySplit((PXCache) lineSplitCache, split);
      }
    }
  }

  protected virtual void VerifyLine(PXCache lineCache, TLine line)
  {
    this.VerifyRow<TLine>(lineCache, line);
  }

  protected virtual void VerifySplit(PXCache lineSplitCache, TLineSplit split)
  {
    this.VerifyRow<TLineSplit>(lineSplitCache, split);
  }

  private void VerifyRow<TRow>(PXCache cache, TRow row) where TRow : class, IBqlTable, new()
  {
    PXNotDecimalUnitException decimalUnitException = PXDBQuantityAttribute.VerifyForDecimal(cache, (object) row);
    if (decimalUnitException == null || decimalUnitException.ErrorLevel < PXErrorLevel.Error)
      return;
    cache.MarkUpdated((object) row);
  }

  protected virtual void UpdateDecimalVerifyMode(TDocument doc)
  {
    DecimalVerifyMode verifyMode = this.HaveHoldStatus(doc) ? DecimalVerifyMode.Warning : DecimalVerifyMode.Error;
    this.Base.Caches<TLine>().Adjust<PXDBQuantityAttribute>().ForAllFields((System.Action<PXDBQuantityAttribute>) (a => a.DecimalVerifyMode = verifyMode));
    this.Base.Caches<TLineSplit>().Adjust<PXDBQuantityAttribute>().ForAllFields((System.Action<PXDBQuantityAttribute>) (a => a.DecimalVerifyMode = verifyMode));
  }

  [PXOverride]
  public int Persist(
    System.Type cacheType,
    PXDBOperation operation,
    Func<System.Type, PXDBOperation, int> basePersist)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(operation, PXDBOperation.Insert, PXDBOperation.Update) || !(cacheType == typeof (TLineSplit)))
      return basePersist(cacheType, operation);
    using (new NotDecimalUnitErrorRedirectorScope<TLineSplit, TLineSplitQty, TLine, TLineQty>((PXCache) this.Base.Caches<TLine>(), new Func<TLineSplit, TLine>(this.LocateLine)))
      return basePersist(cacheType, operation);
  }
}
